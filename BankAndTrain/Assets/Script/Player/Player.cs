using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public JoyStick joyStick, shotStick;
    public ObjectPool pool;
    public GameObject box;
    private float speed, hp, maxHp, Precision;
    private bool isMove;
    private Vector3 moveVec, shotVec, origin;
    private Animator animator;
    private int activeNum;

    void Start()
    {
        pool = GameManager.instance.GetComponent<ObjectPool>();
        box = GameObject.Find("BulletBox");
        animator = GetComponent<Animator>();
        speed = 5;
        Precision = 0;
        maxHp = hp = 10;
        origin = transform.position;
        moveVec = Vector3.zero;
        shotVec = Vector3.zero;
    }

    void Update()
    {
        HandleInput();
        Move();
        Shot();
        AnimatonState();
    }

    private void MaxHpUp(int up)
    {
        maxHp += up;
    }

    private void HpUp(int upHp)
    {
        hp += upHp;
        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void SpeedUp(int upSpeed)
    {
        speed += upSpeed;
    }

    public void LoseHp(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            Debug.Log("die");
        }
    }

    void AnimatonState()
    {
        if(origin != transform.position)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
        animator.SetFloat("MoveX", joyStick.GetXValue());
        animator.SetFloat("MoveY", joyStick.GetYValue());
        animator.SetBool("isMove", isMove);
        origin = transform.position;
    }

    Vector3 PrecisionVec()
    {
        Vector3 pos = Vector3.zero;
        for(int i = 0; i< box.transform.childCount; i++)
        {
            if(box.transform.GetChild(i).gameObject.activeSelf)
            {
                activeNum++;
                if(box.transform.GetChild(i).gameObject.CompareTag("PlayerBullet"))
                {
                    Precision += 0.15f;
                }
            }
        }
        pos = new Vector3(Random.Range(-Precision, Precision), 0, 0);
        return pos;
    }

    void Shot()
    {
        if(shotStick.isShot)
        {
            shotVec = shotStick.GetValue() + PrecisionVec();
            pool.CreateBullet(activeNum, transform.position ,shotVec, "PlayerBullet");
            Precision = 0;
            activeNum = 0;
            shotStick.isShot = false;
        }
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
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            ObjectState os = other.gameObject.GetComponent<ObjectState>();
            switch (os.myType)
            {
            case ObjectState.TYPE.PANCAKE:
                HpUp(1);
                break;
            case ObjectState.TYPE.SAMPAIN:
                SpeedUp(2);
                break;
            case ObjectState.TYPE.BACON:
                MaxHpUp(1);
                break;
            }
            Destroy(other.gameObject);
        }
    }
}
