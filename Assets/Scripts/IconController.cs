using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{

    // public GameObject flyIce;
    public GameObject flyBomb;
    public GameObject flyNormal;

    public GameObject fly;
    public GameObject maggot;

    private static bool toggleFlag;

    public IconController()
    {
        toggleFlag = true;
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

    public Color getIconColor(GameObject icon)
    {
        return new Color(
            icon.GetComponent<Image>().color.r,
            icon.GetComponent<Image>().color.g,
            icon.GetComponent<Image>().color.b);
    }

    public void setIconColor(FlyType type)
    {
        switch(type) {
            case FlyType.DEFAULT:
                fly.GetComponent<Image>().color = getIconColor(flyNormal);
                break;
            case FlyType.BOMB:
                fly.GetComponent<Image>().color = getIconColor(flyBomb);
                break;
            default:
                break;
        }
    }
}
