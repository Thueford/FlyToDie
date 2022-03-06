using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconController : MonoBehaviour
{

    // public GameObject flyIce;
    public GameObject flyBomb;
    public GameObject flyNormal;

    public GameObject fly;
    public GameObject maggot;

    public string flyBombCount;
    public string flyNormalCount;

    private static bool toggleFlag;
    private GameObject normalCounter;
    private GameObject bombCounter;

    public void Awake()
    {
        toggleFlag = false;

        normalCounter = GameObject.FindGameObjectWithTag("NormalCounter");
        normalCounter.GetComponentInParent<TMP_Text>().text = flyNormalCount;

        bombCounter = GameObject.FindGameObjectWithTag("BombCounter");
        bombCounter.GetComponent<TMP_Text>().text = flyBombCount;
    }

    public void SetIcon(bool isFly)
    {
        if (!isFly)
        {
            unselectIcon(fly);
            selectIcon(maggot);
        }
        else
        {
            unselectIcon(maggot);
            selectIcon(fly);
        }
    }
    public static void selectIcon(GameObject icon)
    {
        Debug.Log("selct");
        icon.GetComponent<Image>().color = new Color(1,1,1,1);    
    }

    public static void unselectIcon(GameObject icon)
    {
        Debug.Log("unselect");
        icon.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        
    }

    public void enableIcon(FlyType type)
    {
        switch(type)
        {
            case FlyType.DEFAULT:
                flyNormal.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                break;
            case FlyType.BOMB:
                flyBomb.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                break;
        }
    }

    public Color getIconColor(GameObject icon)
    {
        return new Color(
            icon.GetComponent<Image>().color.r,
            icon.GetComponent<Image>().color.g,
            icon.GetComponent<Image>().color.b);
    }

    public bool setIconColor(FlyType type)
    {
            switch(type) {
                case FlyType.DEFAULT:
                    if( normalCounter.GetComponentInParent<TMP_Text>().text == "0")
                        return false;
                    normalCounter.GetComponentInParent<TMP_Text>().text = (int.Parse(normalCounter.GetComponentInParent<TMP_Text>().text) - 1).ToString();
                    fly.GetComponent<Image>().color = getIconColor(flyNormal);
                    break;
                case FlyType.BOMB:
                    if (bombCounter.GetComponentInParent<TMP_Text>().text == "0")
                        return false;
                    bombCounter.GetComponentInParent<TMP_Text>().text = (int.Parse(bombCounter.GetComponentInParent<TMP_Text>().text) - 1).ToString();
                    fly.GetComponent<Image>().color = getIconColor(flyBomb);
                    break;
            }
            return true;
    }
}
