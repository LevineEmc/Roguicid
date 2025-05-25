using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMangager : MonoBehaviour
{
    //panel
    public GameObject exitPanel;
    public GameObject buffPanel;
    public GameObject bossCounter;
    public GameObject reason;

    // Start is called before the first frame update
    //buttons
    public GameObject attackButton;
    public GameObject defendButton;
    //decks
    public GameObject deck;
    public GameObject bossDeck;
    //hand handler
    public GameObject bossHandler;
    public GameObject deckHandler;
    //blood manager
    public GameObject bossArmor;
    public GameObject bossMaxBloodBar;
    public GameObject bossCurrentBloodBar;
    public GameObject playerArmor;

    public GameObject bossAttackBar;

    private TMP_Text bossAttackValue;
    private TMP_Text bossMaxHealth;
    private TMP_Text bossCurrentHealth;
    private TMP_Text bossArmorHealth;
    private TMP_Text playerHealth;

    //report
    public GameObject reportBar;
    private AutoReport report;

    //0->player use card, cards attack and effect
    //1->boss status check then attack
    //2->defend
    //3->check player status
    public int status = 0;

    public int bossAttack = 5;
    public int bossHealth = 10;
    private int defeated = 0;
    //private bool init = false;

    void Start()
    {
        //hide buttons
        attackButton.SetActive(false); 
        defendButton.SetActive(false);
        //initial first boss,offer all private variable
        bossAttackValue = bossAttackBar.GetComponent<TMP_Text>();
        bossAttackValue.text =bossAttack.ToString();
        bossMaxHealth = bossMaxBloodBar.GetComponent<TMP_Text>();
        bossMaxHealth.text =bossHealth.ToString();
        bossCurrentHealth = bossCurrentBloodBar.GetComponent<TMP_Text>();
        bossCurrentHealth.text = bossHealth.ToString();
        playerHealth = playerArmor.GetComponent<TMP_Text>();

        bossArmorHealth = bossArmor.GetComponent<TMP_Text>();
        bossArmorHealth.text = 0.ToString();

        //report
        report = reportBar.GetComponent<AutoReport>();

        // initial base
        deck.GetComponent<CardDeckBase>().InitialBase();
        bossDeck.GetComponent<CardDeckBase>().InitialBoss();
        for(int i = 0;i<8;i++)
        deckHandler.GetComponent<HorizontalCardHolder>().AddCardToHand();
        report.AddLine("8 cards added");
        bossHandler.GetComponent<HorizontalCardHolder>().AddCardBoss();
        report.AddLine("Here comes a new challenger");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0:

                attackButton.SetActive(true);
                if (deckHandler.transform.childCount == 0)
                {
                    EndGame("You Don't Have Any Cards Left");
                }
                return;
            case 1:
                if(attackButton.activeSelf)
                attackButton.SetActive(false);
                BossCheck();
                return;
            case 2:
                
                defendButton.SetActive(true);
                return;
            case 3:
                if(defendButton.activeSelf)
                    defendButton.SetActive(false);

                CounterCheck();
               // init = true;
                return;
            

        }

    }
    
    public void UseCard()
    {
        if((SelectedSet.selectedSet.Count) == 0)
        {
            return;
        }
        deck.GetComponent<SelectedSet>().Attack();
        if(status == 0)
        {
            status = (status + 1) % 4;
        }
        
    }

    public void BossCheck()
    {
        if (float.Parse(bossCurrentHealth.GetComponent<TMP_Text>().text) <=0)
        {
            report.AddLine("Current Boss is dead");
            //defeated counter
            defeated++;
            NextBoss();
            
            bossMaxBloodBar.GetComponent<TMP_Text>().text = 20.ToString();
            bossCurrentHealth.GetComponent<TMP_Text>().text = 20.ToString();
            //next boss stage
            status = 0;
            return;
        }
        else
        {
            BossAttack();
        }
        if(status == 1)
        status = (status + 1) % 4;
    }

    public void BossAttack() 
    {

        bossAttack = 10;
        report.AddLine("Boss will attack " + bossAttack.ToString() + " this turn");
    }

    public void Defend()
    {
        /*
        if ((SelectedSet.selectedSet.Count) == 0)
        {
            return;
        }
        */
        int sum = deck.GetComponent<SelectedSet>().Defend();
        report.AddLine("You obtain " + sum.ToString() + " armor this turn");
        if(status == 2)
        status = (status + 1) % 4;
    }

    public void CounterCheck()
    {

        int result = int.Parse(playerHealth.text);
        result -=bossAttack;
        playerHealth.text = result.ToString();
        if (result < 0)
        {
            report.AddLine("You are dead");
            EndGame("Boss Penetrated Your Armor");
            
        }
        
        if(status ==3)
        status = (status + 1) % 4;
        

        
    }

    

    public void NextBoss()
    {
        if(bossHandler.transform.childCount!=0)
        Destroy(bossHandler.transform.GetChild(0).gameObject);
        try{
            bossHandler.GetComponent<HorizontalCardHolder>().AddCardBoss();
            report.AddLine("Here comes a new challenger!");

        }
        catch (Exception e)
        {
            report.AddLine("You Win");
            EndGame("Congratulations");
        }
        
        
        
    }

    public void EndGame(string s)
    {
        exitPanel.SetActive(true);
        bossCounter.GetComponent<TMP_Text>().text = defeated.ToString();
        reason.GetComponent<TMP_Text>().text = s;
    }




}
