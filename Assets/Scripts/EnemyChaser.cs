using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public Vector2 jumpHeight = new Vector2(1, 0.5f);
    public float moveCooldown = 0.5f;

    private bool canMove = true;
    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (canMove && player != null)
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
        Vector2 jumpDirection = Vector2.zero;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Move in x direction
            jumpDirection = direction.x > 0 ? new Vector2(jumpHeight.x, -jumpHeight.y) : new Vector2(-jumpHeight.x, jumpHeight.y);
        }
        else
        {
            // Move in y direction
            jumpDirection = direction.y > 0 ? new Vector2(jumpHeight.x, jumpHeight.y) : new Vector2(-jumpHeight.x, -jumpHeight.y);
        }

        targetPosition += jumpDirection;
        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }
}
