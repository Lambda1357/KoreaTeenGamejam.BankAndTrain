using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public JoyStick joyStick, shotStick;
    public GameObject Bullet;
    private List<GameObject> bulletList = new List<GameObject>();
    private float speed, hp, maxHp;
    private bool isMove;
    private Vector3 moveVec, shotVec, origin;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 5;
        maxHp = hp = 10;
        origin = transform.position;
        moveVec = Vector3.zero;
        shotVec = Vector3.zero;
        GameObject box = new GameObject("BulletBox");
        for(int i = 0; i<10; i++)
        {
            GameObject temp = Instantiate(Bullet, Vector3.zero, Quaternion.identity);
            temp.SetActive(false);
            bulletList.Add(temp);
        }
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

    void Shot()
    {
        if(shotStick.isShot)
        {
            shotVec = shotStick.GetValue();
            for(int i = 0; i<bulletList.Count; i++)
            {
                if(!bulletList[i].gameObject.activeSelf)
                {
                    Bullet bull = bulletList[i].GetComponent<Bullet>();
                    bull.SetVec(shotVec);
                    float angle = (float)Mathf.Atan2(shotVec.y, shotVec.x) * Mathf.Rad2Deg + 90;
                    bulletList[i].transform.position = transform.position;
                    bulletList[i].transform.rotation = Quaternion.Euler(0,0,angle);
                    bulletList[i].SetActive(true);
                    shotStick.isShot = false;
                    return;
                }
            }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Object"))
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
        }
    }
}
