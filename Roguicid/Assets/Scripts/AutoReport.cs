using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AutoReport : MonoBehaviour
{
    public GameObject text;
    private TMP_Text textMsg;

    // Start is called before the first frame update
    void Start()
    {
        textMsg = text.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddLine(string s)
    {
        textMsg.text += " *"+s+"\n";
        UpdateToTail();
    }
    public void UpdateToTail()
    {
        this.GetComponent<ScrollRect>().verticalNormalizedPosition = 0; 
        //this.GetComponent<ScrollRect>().verticalScrollbar.value = -0.1f;
    }
}
