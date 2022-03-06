using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{

    public GameObject flyIce;
    public GameObject flyBomb;
    public GameObject flyNormal;

    public GameObject fly;
    public GameObject maggot;

    private static bool toggleFlag;

    private static Color normalColor;
    private static Color iceColor;
    private static Color bombColor;

    public IconController()
    {
        toggleFlag = true;

        normalColor = new Color(
            flyNormal.GetComponent<Image>().color.r,
            flyNormal.GetComponent<Image>().color.g,
            flyNormal.GetComponent<Image>().color.b );
        iceColor = new Color(
            flyIce.GetComponent<Image>().color.r,
            flyIce.GetComponent<Image>().color.g,
            flyIce.GetComponent<Image>().color.b );
        bombColor = new Color(
            flyBomb.GetComponent<Image>().color.r,
            flyBomb.GetComponent<Image>().color.g,
            flyBomb.GetComponent<Image>().color.b );
    }

    public void toggleIconSelect()
    {
        Debug.Log("Toggle Icon");
        if (toggleFlag)
        {
            toggleFlag = false;
            unselectIcon(fly);
            selectIcon(maggot);
        }
        else
        {
            toggleFlag = true;
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

    public void setIconColor(FlyType type)
    {
        switch(type) {
            case FlyType.DEFAULT:
                fly.GetComponent<Image>().color = normalColor;
                break;
            case FlyType.BOMB:
                fly.GetComponent<Image>().color = bombColor;
                break;
            case FlyType.ICE:
                fly.GetComponent<Image>().color = iceColor;
                break;
            default:
                break;
        }
    }
}
