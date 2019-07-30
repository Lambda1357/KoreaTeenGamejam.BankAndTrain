using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public JoyStick joyStick;
    public GameObject Bullet;
    private float speed;
    private Vector3 moveVec, shotVec;

    void Start()
    {
        speed = 5;
        moveVec = Vector3.zero;
        shotVec = Vector3.zero;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        moveVec = PoolInput();
    }

    Vector3 PoolInput()
    {
        float x = joyStick.GetXValue();
        float y = joyStick.GetYValue();
        moveVec = new Vector3(x,y,0).normalized;
        return moveVec;
    } 

    void Move()
    {
        transform.Translate(moveVec * speed * Time.deltaTime);
    }
}
