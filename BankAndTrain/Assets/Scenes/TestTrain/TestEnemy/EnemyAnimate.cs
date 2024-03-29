﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    protected Animator animator;
    GameObject movement;

    Vector3 prvPosition;
    Vector3 curPosition;

    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
        prvPosition = curPosition = Vector3.zero;
        movement = transform.Find("Movement").gameObject;
    }

    // Update is called once per frame
    protected void Update()
    {
        curPosition = movement.transform.position;
        var diraction = curPosition - prvPosition;

        animator.SetFloat("movementX", diraction.x);
        animator.SetFloat("movementY", diraction.z);
    }
}
