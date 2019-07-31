using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRoom : MonoBehaviour
{
    public List<GameObject> boxObj = new List<GameObject>();
    private int boxCnt;

    void Start()
    {
        boxCnt = Random.Range(2,5);
        for (int i = 0; i < boxCnt; i++)
        {
            int num = Random.Range(0,boxObj.Count);
            boxObj[num].SetActive(true);
            boxObj.RemoveAt(num);
        }
    }
}
