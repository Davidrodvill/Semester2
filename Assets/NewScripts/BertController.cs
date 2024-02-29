using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class BertController : MonoBehaviour
{
    NextLevels nextLevels;
    public Tilemap tilemap;
    public Tile targetTile; // The tile to change to when Bert moves
    public float moveSpeed = 5f; // Speed of the jump
    public Vector2 jumpHeight = new Vector2(1, 0.5f); // Height and length of the jump
    public float moveCooldown = 0.5f; // Cooldown in seconds between moves
    public int BertLives = 3; // Number of lives Bert starts with
    public Vector2 BertRespawn; // Position where Bert respawns after losing a life
    public TMP_Text winText;
    AudioSource audiosource;
    Animator animator; // Animator component attached to Bert
    public AudioClip jumpsound;

    public GameObject bertlife1, bertlife2, bertlife3;
   
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private HashSet<Vector3Int> tilesToChange; // Set of tile positions that need to be changed
    private bool canMove = true;

    void Start()
    {
        canMove = true;
        BertRespawn = transform.position; // Set respawn to initial position
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        InitializeTilesToChange();
        animator = GetComponent<Animator>();
        audiosource= GetComponent<AudioSource>();
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
            StartCoroutine(MovePlayer(new Vector2(0.6f, 0.9f))); // Move up-right
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MovePlayer(new Vector2(-0.6f, -0.9f))); // Move down-left
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MovePlayer(new Vector2(-0.6f, 0.9f))); // Move up-left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MovePlayer(new Vector2(0.6f, -0.9f))); // Move down-right
        }
    }

    

    IEnumerator MovePlayer(Vector2 direction)
    {

        if (canMove)
        {
            canMove = false;

            // Trigger the jump animation
            animator.SetBool("isJumping", true);
            audiosource.PlayOneShot(jumpsound);


            // Convert the direction to isometric
            Vector3 isoDirection = new Vector3(direction.x, direction.y, 0);
            Vector3 targetWorldPosition = transform.position + isoDirection;

            // Move Bert to the new position
            while ((Vector2)transform.position != (Vector2)targetWorldPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetWorldPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            animator.SetBool("isJumping", false);
            ChangeTileAtCurrentPosition();
            yield return new WaitForSeconds(moveCooldown);
            canMove = true;
        }
    }

    
    void ChangeTileAtCurrentPosition()
    {
        // Use the tile position from when Bert starts entering the tile's top area
        Vector3Int tilePosition = tilemap.WorldToCell(transform.position - new Vector3(0, tilemap.cellSize.y / 2, 0));

        if (tilesToChange.Contains(tilePosition))
        {
            tilemap.SetTile(tilePosition, targetTile);
            tilemap.RefreshTile(tilePosition);
            tilesToChange.Remove(tilePosition); // Remove the tile position from the set

            // Check for win condition
            if (tilesToChange.Count == 0)
            {
                // Player wins, all tiles have been changed
                Win();
            }
        }

        // Use the tile position from when Bert starts entering the tile's top area
        /*Vector3Int tilePosition = tilemap.WorldToCell(transform.position - new Vector3(0, tilemap.cellSize.y / 2, 0));
        tilemap.SetTile(tilePosition, targetTile);
        tilemap.RefreshTile(tilePosition);
        */
    }

    void Win()
    {
        canMove = false; // Prevent further movement
        // Display win text or handle the win scenario
        winText.gameObject.SetActive(true);
        winText.text = "You Win!";
        // Here you could also trigger a win animation or sound if you have one
        // animator.SetTrigger("WinTrigger");

        // Optionally, load the next level or scene after a delay
        // StartCoroutine(LoadNextLevel());
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
        yield return new WaitForSeconds(30f);
    }
}
