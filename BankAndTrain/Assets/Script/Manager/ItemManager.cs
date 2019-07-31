using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;
    public List<GameObject> items = new List<GameObject>();
    public static List<GameObject> itemList = new List<GameObject>();
    public GameObject box;
    private int boxCnt;
    private Vector3 boxPos;
    private int allBoxCount;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

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
