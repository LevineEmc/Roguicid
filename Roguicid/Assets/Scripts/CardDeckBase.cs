using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardDeckBase : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    private readonly System.Random _random = new System.Random();
    public Sprite[] images;
    public HashSet<int> cards = new HashSet<int>();
    public GameObject deck = null;
    private CardDeckBase cardDeck;

    public GameObject notice;
    private TMP_Text contains;

    public void OnPointerEnter(PointerEventData eventData)
    {
        contains.text = (cards.Count).ToString();
        notice.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        notice.SetActive(false);
    }

    void Start()
    {
       if(deck != null)
        {
            cardDeck = deck.GetComponent<CardDeckBase>();
        }

       contains = notice.transform.GetChild(0).GetComponent<TMP_Text>();
    }



    public int GetCardNo()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Number pool is exhausted");
        }
        // 转换为数组提高随机访问性能
        int[] numberArray = new int[cards.Count];
        cards.CopyTo(numberArray);

        // 生成随机索引
        int randomIndex = _random.Next(0, numberArray.Length);
        int selectedNumber = numberArray[randomIndex];

        // 移除已选数字
        cards.Remove(selectedNumber);

        return selectedNumber;
    }
    public Sprite GetCardImage(int i)
    {
        return images[i];
    }
    public void InitialBase()
    {
        for (int i = 0; i < 40; i++)
        {
            cards.Add(i);
        }
    }

    public void InitialBoss()
    {
        for (int i = 0; i < 12; i++)
        {
            cards.Add(i);
        }
    }

    public void AddCard(int i)
    {
        cards.Add(i);
    }

    public void recoveryDeck(int num)
    {
        //extract form this set ,add to another set
        
            for (int i = 0; i < num ; i++)
            {
                if (cards.Count != 0)
                {
                    cardDeck.AddCard(this.GetCardNo());
                }
                else
            {
                return;
            }
            }
    }

    public  void PrintHashSet()
    {
        if (cards == null || cards .Count == 0)
        {
            Debug.Log("HashSet is empty");
            return;
        }

        Debug.Log($"HashSet: [{string.Join(", ", cards)}]");
    }

}
