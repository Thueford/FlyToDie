using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
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
}
