using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public Vector2 jumpHeight = new Vector2(1, 0.5f);
    public float moveCooldown = 0.5f;
    public Collider2D boundsCollider; // Collider that defines the playable area
    public float initialDelay = 1f;

    private bool canMove = false;
    private Vector2 targetPosition;
    private bool initialMoveDone = false;

    void Start()
    {
        targetPosition = transform.position;
        StartCoroutine(InitialDelay());
    }

    public IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        canMove = true; // After the delay, allow movement
    }

    void Update()
    {
        if (canMove && player != null && !initialMoveDone)
        {
            StartCoroutine(ChasePlayer());
            initialMoveDone = true;
        }
        else if (canMove && player != null)
        {
            StartCoroutine(ChasePlayer());
        }
    }

    private IEnumerator ChasePlayer()
    {
        canMove = false;

        // Calculate the direction towards the player
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;

        // Determine the jump direction based on the player's relative position
        Vector2 jumpDirection = DetermineJumpDirection(direction);

        // Check if the potential target position is within the bounds
        Vector2 potentialTarget = (Vector2)transform.position + jumpDirection;
        if (boundsCollider == null || boundsCollider.OverlapPoint(potentialTarget))
        {
            targetPosition += jumpDirection;
            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

    private Vector2 DetermineJumpDirection(Vector2 direction)
    {
        // Modify this method to suit your isometric movement needs
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Move in x direction
            return direction.x > 0 ? new Vector2(jumpHeight.x, -jumpHeight.y) : new Vector2(-jumpHeight.x, jumpHeight.y);
        }
        else
        {
            // Move in y direction
            return direction.y > 0 ? new Vector2(jumpHeight.x, jumpHeight.y) : new Vector2(-jumpHeight.x, -jumpHeight.y);
        }
    }
}

