using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomState : MonoBehaviour
{
    private List<int> number = new List<int>();
    public List<Box> boxObj = new List<Box>();
    public static int boxCnt;

    void Start()
    {
        for(int i=0;i<boxObj.Count;i++)
        {
            number.Add(i);
        }
        for (int i = 0; i < boxCnt; i++)
        {
            int num = Random.Range(0,number.Count-1);
            boxObj[number[num]].gameObject.SetActive(true);
            number.RemoveAt(num);
        }
    }

    public bool Reserch()
    {
        int num = 0;
        for(int i = 0; i < boxObj.Count; i++)
        {
            if(boxObj[i].isOpen)
            {
                num++;
            }
        }
        if(num == boxCnt)
            return true;
        else
            return false;
    }

}
