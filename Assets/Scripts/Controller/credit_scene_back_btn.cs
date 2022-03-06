using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class credit_scene_back_btn : MonoBehaviour
{
    public void credits_btn_pressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
