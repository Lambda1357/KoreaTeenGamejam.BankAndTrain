using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : EnemyAnimate
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        animator = transform.Find("sprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
