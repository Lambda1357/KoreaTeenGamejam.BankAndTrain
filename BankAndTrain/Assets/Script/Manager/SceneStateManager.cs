using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{

    public int index = 1;// 요걸 조정해서 이동

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int index = scene.buildIndex;
        SceneManager.LoadScene(index+1);
        
        if(index == 1)
        {
            RoomState.boxCnt = Random.Range(4, GameManager.instance.itemManager.itemList.Count/2);
        }   
        else
        {
            RoomState.boxCnt = GameManager.instance.itemManager.itemList.Count;
        }  
    }

    public void LoadSceneByIndex()
    {
        SceneManager.LoadScene(index);
        RoomState.boxCnt = Random.Range(2,4);
    }

}
