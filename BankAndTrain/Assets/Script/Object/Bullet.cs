using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 moveVec;
    private float LifeTime, LiveTime;
    private float speed;

    void Start()
    {
        LifeTime = 1.5f;
        LiveTime = 0;
        speed = 15f;
    }

    public void SetVec(Vector3 pos)
    {
        moveVec = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        LiveTime += Time.deltaTime;
        if(LifeTime <= LiveTime)
        {
            LiveTime = 0;
            gameObject.SetActive(false);
        }
        transform.position += moveVec * Time.deltaTime * speed;
    }
}
