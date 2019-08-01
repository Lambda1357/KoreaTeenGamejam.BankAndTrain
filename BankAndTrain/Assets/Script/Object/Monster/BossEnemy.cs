using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    Transform patrollPoint;
    Animator animator;
    GameObject sprite;

    [Header("Patroll Range")]
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;

    [SerializeField] float remainingDistence;
    [SerializeField] float sliceDistence;
    [SerializeField] float spinDistence;

    enum Status
    {
        Patroll, chase, smash, slice, spin
    }
    Status status;

    protected override void Start()
    {
        base.Start();
        sprite = transform.Find("sprite").gameObject;
        animator = sprite.GetComponent<Animator>();
        patrollPoint.position = RandomPositionInRange();
    }

    Vector3 RandomPositionInRange()
    {
        return new Vector3(Random.Range(minPos.x, maxPos.x), 0, Random.Range(minPos.z, maxPos.z));
    }

    void Update()
    {
        switch (status)
        {
            case Status.Patroll:
                setter.target = patrollPoint;
                DisableAllSkill();

                if (Vector3.Distance(setter.target.position, transform.position) <= remainingDistence)
                    status = Status.chase;
                break;
            case Status.chase:
                setter.target = target.transform;
                DisableAllSkill();

                if (Vector3.Distance(setter.target.position, transform.position) <= remainingDistence)
                    status = Status.smash;
                break;
            case Status.smash:
                DisableAllSkill();
                animator.SetBool("isDoingSkill1", true);

                if ((transform.position - target.transform.position).magnitude < sliceDistence)
                    status = Status.slice;
                else
                {
                    patrollPoint.position = RandomPositionInRange();
                    status = Status.Patroll;
                }

                break;
            case Status.slice:
                DisableAllSkill();
                animator.SetBool("isDoingSkill2", true);
                if ((transform.position - target.transform.position).magnitude < spinDistence)
                    status = Status.spin;
                else
                {
                    patrollPoint.position = RandomPositionInRange();
                    status = Status.Patroll;
                }

                break;
            case Status.spin:
                DisableAllSkill();
                animator.SetBool("isDoingSkill3", true);
                status = Status.Patroll;
                break;
        }
        Debug.Log(status);
        SyncSpriteToAgent();
    }

    void SyncSpriteToAgent()
    {
        sprite.transform.position = transform.position;
    }

    void DisableAllSkill()
    {
        animator.SetBool("isDoingSkill1", false);
        animator.SetBool("isDoingSkill3", false);
        animator.SetBool("isDoingSkill2", false);
    }
}
