using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class QbertMoving : MonoBehaviour
{
    public Level1Manager levelManager;
    private Tilemap tilemap;
    BerthrolotltoltLives bertoltlifescript;
    public float moveSpeed = 5f; // Speed of the jump
    public Vector2 jumpHeight = new Vector2(1, 0.5f); // Height and length of the jump
    public float moveCooldown = 0.5f; // Cooldown in seconds between moves
    public Collider2D boundsCollider; // Collider that defines the playable area
    public Vector2 respawnPosition;

    private bool canMove = true;
    private Vector2 targetPosition;

    void Start()
    {
        levelManager = FindObjectOfType<Level1Manager>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        bertoltlifescript = GetComponent<BerthrolotltoltLives>();
        respawnPosition = transform.position;
        targetPosition = transform.position;
    }

    void Update()
    {
        
        if (canMove)
        {
            CheckInput();
            
        }
    }

    void ChangeTile(Vector3Int tilePosition)
    {
        if (levelManager != null)
        {
            // Additional debug information
            Debug.Log($"ChangeTile called with position {tilePosition}");
            levelManager.PlayerChangedTile(tilePosition);
        }
        else
        {
            Debug.LogError("LevelManager reference not set in QbertMoving script.");
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MovePlayer(new Vector2(jumpHeight.x, jumpHeight.y)));
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MovePlayer(new Vector2(-jumpHeight.x, -jumpHeight.y)));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MovePlayer(new Vector2(-jumpHeight.x, jumpHeight.y)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MovePlayer(new Vector2(jumpHeight.x, -jumpHeight.y)));
        }
       
    }

    public IEnumerator MovePlayer(Vector2 direction)
    {
        canMove = false;
        Vector2 startPosition = transform.position; // Store the start position
        targetPosition += direction;

        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // After moving, convert the player's world position to a cell position in the tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position);

        // Debugging output
        Debug.Log($"Attempting to change tile at {tilePosition}");

        // Call the method to attempt to change the tile
        ChangeTile(tilePosition);

        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }
    public void Respawn()
    {
        StopAllCoroutines(); // Stop any movement coroutines
        transform.position = respawnPosition; // Move bert to the safe respawn position
        targetPosition = respawnPosition; // Reset the berts position to prevent unwanted movement
        canMove = true; // Allow movement again
    }

}
