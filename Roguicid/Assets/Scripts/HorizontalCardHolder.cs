using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;


public class HorizontalCardHolder : MonoBehaviour
{
    
    public Sprite image;
    public int handLimit = 8;
    //source card base
    public GameObject deckBase;
    public GameObject bossBase;
    //discard
    public GameObject discardBase;
    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 2;
    public List<Card> cards;

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    void Start()
    {
        for (int i = 0; i < cardsToSpawn; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        int cardCount = 0;

        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            cardCount++;
        }

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].cardVisual != null)
                    cards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }
    }

    public void AddCardBoss()
    {
        AddBossProcess();
    }
    private void AddBossProcess()
    {
        CardDeckBase cdb = bossBase.GetComponent<CardDeckBase>();
        int number = cdb.GetCardNo();
        image = cdb.GetCardImage(number);

        GameObject obj = Instantiate(slotPrefab, transform);
        CardAttrbute cardAttrbute = obj.AddComponent<CardAttrbute>();
        cardAttrbute.type = number / 3;
        cardAttrbute.points = number % 3;
        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        int cardCount = 0;


        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            card.image = image;
            card.selectable = false;


            cardCount++;
        }

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].cardVisual != null)
                    cards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }


    }


    public void AddCardToHand()
    {
        //Debug.Log(cards.Count);
        if((cards.Count)>handLimit){
            return;
        }
        AddCardProcess();
    }
    private void AddCardProcess()
    {
        
        CardDeckBase cdb = deckBase.GetComponent<CardDeckBase>();
        int number = cdb.GetCardNo();
        

        GameObject obj = Instantiate(slotPrefab, transform);
        CardAttrbute cardAttrbute = obj.AddComponent<CardAttrbute>();
        cardAttrbute.type = number / 10;
        cardAttrbute.points = number % 10;

        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();
        /*
        int cardCount = 0;

        
        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            image = cdb.GetCardImage(number);
            card.image = image;
            
            
            cardCount++;
        }
        */
        cards[cards.Count - 1].PointerEnterEvent.AddListener(CardPointerEnter);
        cards[cards.Count - 1].PointerExitEvent.AddListener(CardPointerExit);
        cards[cards.Count - 1].BeginDragEvent.AddListener(BeginDrag);
        cards[cards.Count - 1].EndDragEvent.AddListener(EndDrag);
        cards[cards.Count - 1].name = "0";//cardCount.ToString();
        image = cdb.GetCardImage(number);
        cards[cards.Count - 1].image = image;
        

        StartCoroutine(Frame());

        
        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].cardVisual != null)
                    cards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }
       
    }
    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }

    void EndDrag(Card card)
    {
        if (selectedCard == null)
            return;

        selectedCard.transform.DOLocalMove(selectedCard.selected ? new Vector3(0,selectedCard.selectionOffset,0) : Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

        rect.sizeDelta += Vector2.right;
        rect.sizeDelta -= Vector2.right;

        selectedCard = null;

    }

    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (hoveredCard != null)
            {
                Destroy(hoveredCard.transform.parent.gameObject);
                cards.Remove(hoveredCard);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //AddCardToDeck();
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (selectedCard == null)
            return;

        if (isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    

                    Swap(i);
                    break;
                }
            }

            if (selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        isCrossing = true;

        Transform focusedParent = selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        int tempType = focusedParent.GetComponent<CardAttrbute>().type;
        int tempPoints = focusedParent.GetComponent<CardAttrbute>().points;
        focusedParent.GetComponent<CardAttrbute>().type = crossedParent.GetComponent<CardAttrbute>().type;
        focusedParent.GetComponent<CardAttrbute>().points = crossedParent.GetComponent<CardAttrbute>().points;
        crossedParent.GetComponent<CardAttrbute>().type = tempType;
        crossedParent.GetComponent<CardAttrbute>().points = tempPoints;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].selected ? new Vector3(0, cards[index].selectionOffset, 0) : Vector3.zero;
        selectedCard.transform.SetParent(crossedParent);

        isCrossing = false;

        if (cards[index].cardVisual == null)
            return;

        bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in cards)
        {
            card.cardVisual.UpdateIndex(transform.childCount);
        }
    }

}
