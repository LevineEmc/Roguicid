using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffSelection : MonoBehaviour
{
    //public GameObject buffChooseMenu;
    public GameObject cardHolder;
    public GameObject armor;

    private HorizontalCardHolder deckHandler;
    private TMP_Text armorValue;

    // Start is called before the first frame update
    void Start()
    {
        deckHandler = cardHolder.GetComponent<HorizontalCardHolder>();
        armorValue = armor.GetComponent<TMP_Text>();
    }

    public void GainArmor()
    {
        // gain 10 armors
        int current = int.Parse(armorValue.text);
        armorValue.text = (current+10).ToString();
        CloseChooseMenu();
    }
    public void DrawCards()
    {
        for (int i = 0; i < 5; i++)
        {
            deckHandler.AddCardToHand();
        }
        CloseChooseMenu();
    }

    public void EnhanceLimit()
    {
        deckHandler.handLimit++;
        DrawCardsController.limit++;
        CloseChooseMenu();
    }
    public void CloseChooseMenu() 
    {
        this.gameObject.SetActive(false);
    }
}
