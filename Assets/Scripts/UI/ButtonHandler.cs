using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void changeSceneBtn(int sceneId)
    {
        Debug.Log($"Load Scene with Id {sceneId}");
        SceneManager.LoadScene(sceneId);
    }

    public void changeSceneBtn(string sceneName)
    {
        Debug.Log($"Load Scene with Name {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void loadNextScene()
    {
        int nextSceneId = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log($"Load Scene with Name {nextSceneId}");
        SceneManager.LoadScene(nextSceneId);
    }

    public void pause_click()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
    }

    public void resume_click()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
    }

    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit(); 
    }
}
