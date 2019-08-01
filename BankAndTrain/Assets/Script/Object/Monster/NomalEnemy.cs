using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalEnemy : Enemy
{
    public GameObject poses;
    private List<Transform> objects = new List<Transform>();
    private Transform hide;
    private float redayTime, shootTime;

    protected override void Start()
    {
        base.Start();
        hp = 3;
        range = 8;
        shootTime = 1.0f;
        for(int i =0; i<poses.transform.childCount; i++)
        {
            objects.Add(poses.transform.GetChild(i).transform);
        }
    }

    Transform FindNearest()
    {
        Transform temp = objects[0];
        float near = Vector3.Distance(objects[0].position, transform.position);

        for(int i = 1; i < objects.Count; i++)
        {
            if(near > Vector3.Distance(objects[i].position, transform.position))
            {
                near = Vector3.Distance(objects[i].position, transform.position);
                temp = objects[i];
            }
        }
        return temp;
    }

    void Find()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance <= range)
        {
            ms = MONSTER_STATE.HIDE;
            hide = FindNearest();
            setter.target = hide.transform;
        }
    }

    void Hide()
    {
        distance = Vector3.Distance(hide.position, transform.position);
        if(distance <= 0.3f)
        {
            ms = MONSTER_STATE.SHOT;
        }
    }

    void Shot()
    {
        redayTime += Time.deltaTime;
        if(redayTime >= shootTime)
        {
            redayTime = 0;
            if(transform.position.x < 0)
            {
                transform.position += Vector3.right;
            }
            else
            {
                transform.position += Vector3.left;
            }

            objectPool.CreateBullet(1, transform.position + Vector3.down, (target.transform.position - transform.position).normalized, "EnemyBullet");
        }

        if(range <= Vector3.Distance(target.transform.position, transform.position))
        {
            ms = MONSTER_STATE.FIND;
            setter.target = target;
        }
    }


    void FixedUpdate()
    {
        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }

        switch (ms)
        {
        case MONSTER_STATE.FIND:
            Find();
            break;
        case MONSTER_STATE.HIDE:
            Hide();
            break;
        case MONSTER_STATE.SHOT:
            Shot();
            break;
        }
    }
}
