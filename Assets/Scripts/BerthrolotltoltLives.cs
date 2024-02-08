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
        
    }
    IEnumerator Bertdied()
    {
        if (BertLives <= 0)
        {
            //some text saying he died or sum

            yield return new WaitForSeconds(3);
            //restart scene
            SceneManager.LoadScene("SampleScene");
        }
    }
}
