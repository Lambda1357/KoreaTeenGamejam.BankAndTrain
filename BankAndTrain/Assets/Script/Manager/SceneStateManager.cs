using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int index = scene.buildIndex;
        SceneManager.LoadScene(index+1);
    }

    public void LoadGuestScene()
    {
        SceneManager.LoadScene("GuestRoom");
    }

    public void LoadKitchenScene()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void LoadCarbaygoScene()
    {
        SceneManager.LoadScene("Carbaygo");
    }

    public void LoadEngineScene()
    {
        SceneManager.LoadScene("EngineRoom");
    }


}
