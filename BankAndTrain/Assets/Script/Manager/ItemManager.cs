using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Sprite> endItem = new List<Sprite>();
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> itemList = new List<GameObject>();
    private Vector3 boxPos;
    private int allBoxCount;

    void Start()
    {
        allBoxCount = Random.Range(10,20);
        for(int i = 0; i < allBoxCount; i++)
        {
            itemList.Add(items[Random.Range(0,items.Count-2)]);
        }
        itemList.Add(items[items.Count-1]);
    }

    public void Set()
    {
        allBoxCount = Random.Range(10,20);
        for(int i = 0; i < allBoxCount; i++)
        {
            itemList.Add(items[Random.Range(0,items.Count-2)]);
        }
        itemList.Add(items[items.Count-1]);
    }
}
