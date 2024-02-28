using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevels : MonoBehaviour
{
    BertController bertController;
    // Start is called before the first frame update
    void Start()
    {
        //connecting to other script
        bertController = GetComponent<BertController>();
    }

    // Update is called once per frame
    void Update()
    {
        //game
    }
    public void level2()
    {
        //make the gmae go into level 2
    }
    public void level3()
    {

    }
    public void level4()
    {
        
    }
}
