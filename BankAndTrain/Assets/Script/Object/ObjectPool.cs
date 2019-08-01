using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> bulletList = new List<GameObject>();
    public GameObject Bullet;
    private GameObject box;

    void Start()
    {
        box = new GameObject("BulletBox");
        for(int i = 0; i<20; i++)
        {
            GameObject temp = Instantiate(Bullet, Vector3.zero, Quaternion.identity);
            temp.SetActive(false);
            temp.transform.SetParent(box.transform);
            bulletList.Add(temp);
        }
        DontDestroyOnLoad(box);
    }

    public void CreateBullet(int number, Vector3 startPos, Vector3 shotVec, string tag)
    {
        //위치
        Bullet bull = bulletList[number].GetComponent<Bullet>();
        bull.SetVec(startPos, shotVec);
        
        //회전
        float angle = (float)Mathf.Atan2(shotVec.y, shotVec.x) * Mathf.Rad2Deg + 90;
        bulletList[number].transform.rotation = Quaternion.Euler(0,0,angle);

        //태그달고 발사
        bulletList[number].gameObject.tag = tag;
        bulletList[number].SetActive(true);
    }
}
