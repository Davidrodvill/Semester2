using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevels : MonoBehaviour
{
    //gameplan
    public bool winBool = false;
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
    public void GoToNextLevel()
    {
        if (winBool)
        {
            // Determine the current scene
            string currentSceneName = SceneManager.GetActiveScene().name;
            Debug.Log($"Current Scene: {currentSceneName}. Preparing to load next level.");

            // Depending on the current scene, load the next one
            if (currentSceneName == "SampleScene")
            {
                SceneManager.LoadScene("Level 2");
            }
            else if (currentSceneName == "Level 2")
            {
                SceneManager.LoadScene("Level 3");
            }
            else if (currentSceneName == "Level 3")
            {
                SceneManager.LoadScene("Level 4");
            }
            else if (currentSceneName == "Level 4")
            {
                // If it's the last level, you could go to a win screen or loop back to the start
                //SceneManager.LoadScene("WinScene");
            }
        }
        else
        {
            Debug.Log("GoToNextLevel called, but winBool is false.");
        }
    }
}
