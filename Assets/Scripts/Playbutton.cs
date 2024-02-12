using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InfoButtonHit()
    {
        SceneManager.LoadScene("InfoScene");
    }
    public void MenuButtonHit()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void PlayButtonHit()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ControlsButtonHit()
    {
        SceneManager.LoadScene("ControlScene");
    }
}
