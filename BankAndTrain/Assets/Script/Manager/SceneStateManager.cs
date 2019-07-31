using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    private int boxCnt;
    
    void Start()
    {
        int num = ItemManager.itemList.Count;
        boxCnt = Random.Range(2,num);    
    }

    
}
