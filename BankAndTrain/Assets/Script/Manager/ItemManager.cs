using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public static List<GameObject> itemList = new List<GameObject>();
    private int allBoxCount;

    void Start()
    {
        allBoxCount = Random.Range(10,20);
 
        for(int i = 0; i < allBoxCount; i++)
        {
            itemList.Add(items[Random.Range(0,items.Count-1)]);
        }
        itemList.Add(items[items.Count-1]);
    }

    


}
