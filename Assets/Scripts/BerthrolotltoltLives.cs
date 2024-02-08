using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BerthrolotltoltLives : MonoBehaviour
{
    public int BertLives = 3;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == null)
        {
            BertLives--;
        }
    }
    IEnumerator Bertdied()
    {
        
        //some text saying he died or sum

        yield return new WaitForSeconds(3);
        //restart scene
        SceneManager.LoadScene("SampleScene");
        
    }
}
