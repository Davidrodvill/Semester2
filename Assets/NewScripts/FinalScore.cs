using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class FinalScore : MonoBehaviour
{
    public TMP_Text finalscoretext;
    // Start is called before the first frame update
    void Start()
    {
        finalscoretext.text = "Your final score is: 16,500.69";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returntomainmenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void quitgame()
    {
        Application.Quit();
    }
}
