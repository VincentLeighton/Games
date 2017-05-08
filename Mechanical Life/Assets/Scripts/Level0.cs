using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level0 : MonoBehaviour
{
    public static int YLANEDIVIDERS = 6;
    public static int XLANEDIVIDERS = 11;
    public static float LANEHEIGHT = 87;
    public static float LANEWIDTH = 60;
    private float[] yLaneBorders;
    private float[] xLaneBorders;
    public Text currencyText;
    private int currency;

    void Awake()
    {
        currencyText = GameObject.Find("Currency").GetComponent<Text>();
        yLaneBorders = new float[YLANEDIVIDERS];
        //1:0, 2:90, 3:180
        for (int i = 0; i <= YLANEDIVIDERS - 1; i++)
        {
            yLaneBorders[i] = LANEHEIGHT * i;
        }
        xLaneBorders = new float[XLANEDIVIDERS];
        for (int i = 0; i <= XLANEDIVIDERS - 1; i++)
        {
            xLaneBorders[i] = LANEWIDTH * i;
        }
    }
    // Use this for initialization
    void Start()
    {
        currency = Int32.Parse(currencyText.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.SelectedDefenderPrefab() != null)
        {
            //Debug.Log("Selected defender "+Button.SelectedDefender().name);
        }
    }

    public bool YPositionInLane(int lane, float y)
    {
        bool sameLane = false;
        if (lane >= 0 && lane <= yLaneBorders.Length - 1)
        {
            if (y >= yLaneBorders[lane] && y < yLaneBorders[lane])
            {
                sameLane = true;
            }
        }
        return sameLane;
    }

    public bool XPositionInLane(int lane, float x)
    {
        bool sameLane = false;
        if (lane >= 0 && lane <= xLaneBorders.Length - 1)
        {
            if (x >= xLaneBorders[lane] && x < xLaneBorders[lane])
            {
                sameLane = true;
            }
        }
        return sameLane;
    }

    public int LaneCharacterIsIn(GameObject g)
    {
        int laneNum = -1;
        float y = g.transform.position.y;
        for (int i = 0; i <= YLANEDIVIDERS; i++)
        {
            if (YPositionInLane(i, y))
            {
                laneNum = i;
            }
        }
        return laneNum;
    }

    public int YLanePositionIsIn(Vector2 pos)
    {
        int laneNum = -1;
        float y = pos.y;
        for (int i = 0; i <= YLANEDIVIDERS; i++)
        {
            if (YPositionInLane(i, y))
            {
                laneNum = i;
            }
        }
        return laneNum;
    }

    public int XLanePositionIsIn(Vector2 pos)
    {
        int laneNum = -1;
        float x = pos.x;
        for (int i = 0; i <= XLANEDIVIDERS; i++)
        {
            if (XPositionInLane(i, x))
            {
                laneNum = i;
            }
        }
        return laneNum;
    }

    public Vector2 GetLaneCords(Vector2 pos)
    {
        return new Vector2(XLanePositionIsIn(pos), YLanePositionIsIn(pos));
    }

    public bool CharactersInSameLane(GameObject character1, GameObject character2)
    {
        return LaneCharacterIsIn(character1) == LaneCharacterIsIn(character2);
    }

    public bool PositionsInSameLane(Vector2 pos1, Vector2 pos2)
    {
        return YLanePositionIsIn(pos1) == YLanePositionIsIn(pos2);
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        currencyText.text = currency.ToString();
    }

    public void SpendCurrency(int amount)
    {
        currency -= amount;
        currencyText.text = currency.ToString();
    }

    public int GetCurrency(){
        return currency;
    }

    public void SnapCharacterToGrid(GameObject character)
    {
        float leftBorder;
        float rightBorder;
        float bottomBorder;
        float topBorder;
        //get the x and y dividers the char is between
        //loop over x's and set which 2 we're between
        for (int i = 0; i < xLaneBorders.Length - 1; i++)
        {
            if (i + 1 <= xLaneBorders.Length)
            {
                if (CharacterBetweenBorders(character.transform.position.x, xLaneBorders[i], xLaneBorders[i + 1]))
                {
                    leftBorder = i;
                    rightBorder = i + 1;
                    //Debug.LogFormat("i:{0} i+1:{1}", i, i + 1);
                }
            }
        }
        //loop over y's and set which 2 we're between
        for (int i = 0; i < yLaneBorders.Length - 1; i++)
        {
            if (i + 1 <= yLaneBorders.Length)
            {
                if (CharacterBetweenBorders(character.transform.position.y, yLaneBorders[i], yLaneBorders[i + 1]))
                {
                    bottomBorder = i;
                    topBorder = i + 1;
                }
            }
        }
    }

    private bool CharacterBetweenBorders(float pos, float border1, float border2)
    {
        //Debug.LogFormat("pos:{0} border1:{1} border2:{2}",pos, border1, border2);
        return pos >= border1 && pos < border2;
    }
}
