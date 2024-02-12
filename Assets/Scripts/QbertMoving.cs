using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QbertMoving : MonoBehaviour
{
   
    public float moveSpeed = 5f; // Speed of the jump
    public Vector2 jumpHeight = new Vector2(1, 0.5f); // Height and length of the jump
    public float moveCooldown = 0.5f; // Cooldown in seconds between moves
    public Collider2D boundsCollider; // Collider that defines the playable area

    private bool canMove = true;
    private Vector2 targetPosition;

    void Start()
    {
       
        targetPosition = transform.position;
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
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(jumpHeight.x, jumpHeight.y);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2(-jumpHeight.x, -jumpHeight.y);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-jumpHeight.x, jumpHeight.y);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(jumpHeight.x, -jumpHeight.y);
        }

        if (direction != Vector2.zero)
        {
            Vector2 potentialTarget = (Vector2)transform.position + direction;
            if (boundsCollider == null || boundsCollider.OverlapPoint(potentialTarget))
            {
                StartCoroutine(MovePlayer(direction));
            }
        }
    }

    public IEnumerator MovePlayer(Vector2 direction)
    {
        canMove = false;
        targetPosition += direction;

        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

}
