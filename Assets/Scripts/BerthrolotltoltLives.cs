using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BerthrolotltoltLives : MonoBehaviour
{
    QbertMoving qbertscript;
    public int BertLives = 3;
    public Vector2 BertRespawn;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        BertRespawn = transform.position;
        rb = GetComponent<Rigidbody2D>();
        qbertscript = GetComponent<QbertMoving>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            LoseLife();
        }
    }
    public void LoseLife()
    {
        BertLives--;
        if (BertLives <= 0 ) 
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        // Reset Q*bert's position to the spawn point.
        transform.position = BertRespawn;

        rb.velocity = Vector2.zero;
        qbertscript.Respawn();

    }

}
