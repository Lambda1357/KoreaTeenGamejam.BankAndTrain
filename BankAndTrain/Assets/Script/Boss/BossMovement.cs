using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Vector3 patrollPoint;
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


    // Start is called before the first frame update
    void Start()
    {
        agent = transform.Find("Movement").GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        sprite = transform.Find("sprite").gameObject;
        animator = sprite.GetComponent<Animator>();
        patrollPoint = RandomPositionInRange();
    }

    Vector3 RandomPositionInRange()
    {
        return new Vector3(Random.Range(minPos.x, maxPos.x), 0, Random.Range(minPos.z, maxPos.z));
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case Status.Patroll:
                agent.destination = patrollPoint;
                DisableAllSkill();

                if (agent.remainingDistance <= remainingDistence)
                    status = Status.chase;
                break;
            case Status.chase:
                agent.destination = player.transform.position;
                DisableAllSkill();

                if (agent.remainingDistance <= remainingDistence)
                    status = Status.smash;
                break;
            case Status.smash:
                DisableAllSkill();
                animator.SetBool("isDoingSkill1", true);

                if ((agent.transform.position - player.transform.position).magnitude < sliceDistence)
                    status = Status.slice;
                else
                {
                    patrollPoint = RandomPositionInRange();
                    status = Status.Patroll;
                }

                break;
            case Status.slice:
                DisableAllSkill();
                animator.SetBool("isDoingSkill2", true);
                if ((agent.transform.position - player.transform.position).magnitude < spinDistence)
                    status = Status.spin;
                else
                {
                    patrollPoint = RandomPositionInRange();
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
        sprite.transform.position = agent.transform.position;
    }

    void DisableAllSkill()
    {
        animator.SetBool("isDoingSkill1", false);
        animator.SetBool("isDoingSkill3", false);
        animator.SetBool("isDoingSkill2", false);
    }
}
