using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttrbute : MonoBehaviour
{
    //0-spade-armor;
    //1-heart-recover;
    //2-diamond-draw;
    //3-club-double
    //range 1-10
    public int type;
    public int points;
    public CardAttrbute(int type, int points)
    {
        this.type = type;
        this.points = points;
    }
}
