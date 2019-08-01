using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    protected enum MONSTER_STATE
    {
        FIND, HIDE, SHOT
    }
    protected MONSTER_STATE ms;
    protected Transform target;
    protected AIDestinationSetter setter;
    protected Vector2 direction;
    protected ObjectPool objectPool;
    protected float distance, range;
    protected float speed;
    protected int hp;

    protected virtual void Start()
    {
        ms = MONSTER_STATE.FIND;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        objectPool = GameManager.instance.GetComponent<ObjectPool>();
        setter = GetComponent<AIDestinationSetter>();
        setter.target = target;
    }
}
