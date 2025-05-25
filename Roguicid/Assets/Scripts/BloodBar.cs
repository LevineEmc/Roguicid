using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maxBar;
    public GameObject currentBar;
    private TMP_Text max;
    private TMP_Text current;
    private float maxHealth;
    private float currentHealth;
    private float rate;
    private Image image;
    void Start()
    {
        max = maxBar.GetComponent<TMP_Text>();
        current = currentBar.GetComponent<TMP_Text>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = float.Parse(max.text);
        currentHealth = float.Parse(current.text);
        
        rate = currentHealth/maxHealth;
        
        image.fillAmount = rate;
         

    }
}
