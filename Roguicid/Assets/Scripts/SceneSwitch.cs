using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //µã»÷Êó±êÓÒ¼üÇÐ»»³¡¾°
        if (Input.GetMouseButtonDown(1))
        {
            

        }

    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
