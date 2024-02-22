using System.Collections;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public Vector2 jumpHeight = new Vector2(1, 0.5f);
    public float moveCooldown = 0.5f;
    public Collider2D boundsCollider; // Collider that defines the playable area
    public float initialDelay = 1f;

    private Vector2 targetPosition;
    private bool isMoving = false; // Tracks if the enemy is currently moving

    void Start()
    {
        targetPosition = transform.position;
        // Start the initial movement after a delay
        Invoke(nameof(StartChasing), initialDelay);
    }

    void StartChasing()
    {
        StartCoroutine(ChasePlayer());
    }

    private IEnumerator ChasePlayer()
    {
        // Wait for the initial delay before starting the chase
        yield return new WaitForSeconds(initialDelay);

        while (player != null)
        {
            if (!isMoving)
            {
                isMoving = true;

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

                // Wait for the move cooldown before the next move
                yield return new WaitForSeconds(moveCooldown);
                isMoving = false;
            }

            // Wait a short time before checking again, to prevent tight loops
            yield return null;
        }
    }

    private Vector2 DetermineJumpDirection(Vector2 direction)
    {
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
