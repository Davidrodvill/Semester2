using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using UnityEngine.UI;

public class BertController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile targetTile; // The tile to change to when Bert moves
    public float moveSpeed = 5f; // Speed of the jump
    public Vector2 jumpHeight = new Vector2(1, 0.5f); // Height and length of the jump
    public float moveCooldown = 0.5f; // Cooldown in seconds between moves
    public int BertLives = 3; // Number of lives Bert starts with
    public Vector2 BertRespawn; // Position where Bert respawns after losing a life
    
    
    public GameObject bertlife1, bertlife2, bertlife3;
    
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private HashSet<Vector3Int> tilesToChange; // Set of tile positions that need to be changed
    private bool canMove = true;

    void Start()
    {
        
        BertRespawn = transform.position; // Set respawn to initial position
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        InitializeTilesToChange();
        
    }

    void InitializeTilesToChange()
    {
        tilesToChange = new HashSet<Vector3Int>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (tilemap.HasTile(localPlace))
            {
                tilesToChange.Add(localPlace);
            }
        }
    }

    void Update()
    {
        if (canMove)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Move(new Vector2(jumpHeight.x, jumpHeight.y));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move(new Vector2(-jumpHeight.x, -jumpHeight.y));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(new Vector2(-jumpHeight.x, jumpHeight.y));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(new Vector2(jumpHeight.x, -jumpHeight.y));
        }
    }

    void Move(Vector2 direction)
    {
        StartCoroutine(MovePlayer(direction));
    }

    IEnumerator MovePlayer(Vector2 direction)
    {
        canMove = false;
        targetPosition += direction;

        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        ChangeTileAtCurrentPosition();
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

    void ChangeTileAtCurrentPosition()
    {
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position);

        if (tilesToChange.Contains(tilePosition))
        {
            tilemap.SetTile(tilePosition, targetTile);
            tilesToChange.Remove(tilePosition);
            tilemap.RefreshTile(tilePosition);

            if (tilesToChange.Count == 0)
            {
                Debug.Log("All tiles changed! Level complete!");
                // Handle level completion here

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            LoseLife();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "StillOn")
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        
        
        BertLives--;
        if(BertLives == 3)
        {
            bertlife1.SetActive(true);
            bertlife2.SetActive(true);
            bertlife3.SetActive(true);
        }
        else if(BertLives == 2)
        {
            bertlife1.SetActive(false);
            bertlife2.SetActive(true);
            bertlife3.SetActive(true);
        }
        else if(BertLives == 1)
        {
            bertlife1.SetActive(false);
            bertlife2.SetActive(false);
            bertlife3.SetActive(true);
        }
        if (BertLives <= 0)
        {
            // Get the current scene name and reload it
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        StopAllCoroutines(); // Stop any ongoing movement coroutines
        transform.position = BertRespawn; // Reset Bert's position
        rb.velocity = Vector2.zero;
        targetPosition = BertRespawn; // Reset the target position
        canMove = true; // Allow movement again
    }
    IEnumerator stopwaitaminute()
    {
        yield return new WaitForSeconds(3);
    }
}
