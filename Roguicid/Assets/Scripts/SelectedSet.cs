using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class SelectedSet : MonoBehaviour
{
    public static List<GameObject> selectedSet = new List<GameObject>();
    public GameObject discardSet;
    public GameObject cardHolder;

    public GameObject current;
    public GameObject playerArmor;
    public GameObject bossArmor;
    private TMP_Text currentHealth;
    private TMP_Text playerHealth;
    private TMP_Text bossArmorHealth;
    private float c;
    private float Parmor;
    private float Barmor;
    private List<GameObject> toBeDestorySet = new List<GameObject>();
    private int drawCards = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = current.GetComponent<TMP_Text>();
        playerHealth = playerArmor.GetComponent<TMP_Text>();
        bossArmorHealth = bossArmor.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCardToDiscard()
    {
        
        foreach (GameObject card in selectedSet)
        {
            //No
            int number = (card.GetComponent<CardAttrbute>().type)*10+ (card.GetComponent<CardAttrbute>().points);
            //move No to discard base
            discardSet.GetComponent<CardDeckBase>().cards.Add(number);
            //modify horizontal list (global)
            cardHolder.GetComponent<HorizontalCardHolder>().cards.Remove(card.GetComponentInChildren<Card>());
            Destroy(card);
            //cardHolder.GetComponent<HorizontalCardHolder>().UpdatedCount();
        }
        selectedSet.Clear();
        //cardHolder.GetComponent<HorizontalCardHolder>().UpdatedCount();


    }
    public void Attack()
    {
        foreach (GameObject card in selectedSet)
        {
            
            toBeDestorySet.Add(card);

        }

        MoveCardToDiscard();
        //blood decrease;
        c = float.Parse(currentHealth.text);
        Barmor = float.Parse(bossArmorHealth.text);
        int attackSum = 0;
        foreach (GameObject card in toBeDestorySet)
        {
            //let card active it's effect
            attackSum+= CardEffect(card.GetComponent<CardAttrbute>().type, card.GetComponent<CardAttrbute>().points + 1);
            
        }
        if (Barmor >= attackSum)
        {
            Barmor -= attackSum;
            bossArmorHealth.text = Barmor.ToString();
        }
        else
        {
            attackSum -= (int)Barmor;
            Barmor = 0;

            c -= attackSum;
            currentHealth.text = c.ToString();
            bossArmorHealth.text = Barmor.ToString();
        }
        

        toBeDestorySet.Clear();
        //MoveCardToDiscard();
        //DrawCards();
    }

    public int Defend()
    {

        Parmor = float.Parse(playerHealth.text);
        int defendSum = 0;
        foreach (GameObject card in selectedSet)
        {
            //let card active it's effect
            defendSum += card.GetComponent<CardAttrbute>().points + 1;

        }
        Parmor += defendSum;
        playerHealth.text = Parmor.ToString();
        MoveCardToDiscard();
        return defendSum;
    }

    public int CardEffect(int type,int point)
    {
        //0-spade-armor;
        //1-heart-recover;
        //2-diamond-draw;
        //3-club-double
        //range 1-10
        Parmor = float.Parse(playerHealth.text);
        int attack = point;
        switch (type)
        {
            case 0:
                Parmor+=point;
                playerHealth.text  = Parmor.ToString();
                break;
            case 1:
                discardSet.GetComponent<CardDeckBase>().recoveryDeck(point);
                break;
            case 2:
                
                for (int i = 0; i < point; i++)
                {
                    //cardHolder.GetComponent<HorizontalCardHolder>().AddCardToHand();
                    //Debug.Log(1);
                    DrawCardsController.IncreaseChanges();
                }
                
                drawCards += point;
                break;
            case 3:
                attack *= 2;
                break;
            default: break;  
        }
        return attack;
    }

    public void DrawCards()
    {
        //cardHolder.GetComponent<HorizontalCardHolder>().UpdatedCount();
        for (int i = 0; i < drawCards; i++)
        {
          cardHolder.GetComponent<HorizontalCardHolder>().AddCardToHand();  
        }
        
        drawCards = 0;
    }
   
}
