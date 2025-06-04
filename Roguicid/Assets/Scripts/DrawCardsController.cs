using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DrawCardsController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int changes = 0;
    public static int limit = 8;
    public GameObject addButton;
    public GameObject limitBar;
    public GameObject changesBar;
    private TMP_Text limitNumber
;   private TMP_Text changeNumber;

    void Start()
    {
        changes = 0;
        limit = 8;
        limitNumber = limitBar.GetComponent<TMP_Text>();
        changeNumber = changesBar.GetComponent<TMP_Text>();
        changeNumber.text = 0.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(changes == 0)
        {
            addButton.SetActive(false);
        }
        else
        {
            addButton.SetActive(true);
        }
        limitNumber.text = limit.ToString();
        changeNumber.text = changes.ToString();
    }
    public void DecreaseChanges()
    {
        changes--;
    }
    public static void IncreaseChanges()
    {
        if (changes < limit)
        {
            changes++;
        }
    }
}
