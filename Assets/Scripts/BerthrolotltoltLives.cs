using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BerthrolotltoltLives : MonoBehaviour
{
    QbertMoving qbertscript;
    public int BertLives = 3;
    public float BertRespawn;
    // Start is called before the first frame update
    void Start()
    {
        qbertscript = GetComponent<QbertMoving>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(BertLives <= 0)
        {
            StartCoroutine(Bertdied());
        }
    }
    //if there is nothing in the space he's on, meaning he jumped off X_X
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "StillOn")
        {
            StartCoroutine(BertLostlife());
        }
        
    }
    IEnumerator BertLostlife()
    {

        transform.position = new Vector2(0.066f, -0.188f);
        //work on progress
        BertLives--;
        
        yield return new WaitForSeconds(0);
    }
    IEnumerator Bertdied()
    {
        
        //some text saying he died or sum

        yield return new WaitForSeconds(3);
        //restart scene
        SceneManager.LoadScene("SampleScene");
        
    }
}
