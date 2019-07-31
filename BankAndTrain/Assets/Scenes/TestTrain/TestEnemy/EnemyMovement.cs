using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    GameObject agentObject;
    NavMeshAgent agent;
    Transform sprite;
    CoverPoint[] coverPoints;
    GameObject player;

    [SerializeField] float maxDistenceForCover;
    [SerializeField] float fireDelay;
    [SerializeField] float distanceFromCoverPoint;
    [SerializeField] int minGunFireCount;

    float curFireDelay;
    int curGunfireCount;

    enum Status
    {
        Find, Hide,Shoot
    }
    Status status;
    

    // Start is called before the first frame update
    void Start()
    {
        agentObject = transform.Find("Movement").gameObject;
        agent = agentObject.GetComponent<NavMeshAgent>();
        sprite = transform.Find("sprite");
        player = GameObject.FindWithTag("Player");

        LoadCoverPoints();
        status = Status.Find;
    }

    void LoadCoverPoints()
    {
        coverPoints = FindObjectsOfType<CoverPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isInCoverDistence;
        isInCoverDistence = maxDistenceForCover >= ((agentObject.transform.position - player.transform.position)).magnitude;

        Debug.Log(isInCoverDistence);
        Debug.Log(((Vector2)(agentObject.transform.position - player.transform.position)).magnitude);

        switch (status)
        {
            case Status.Find:
                agent.destination = player.transform.position;

                if (isInCoverDistence)
                    status = Status.Hide;
                break;

            case Status.Hide:
                CoverPoint closestPoint = coverPoints[0];
                foreach(var point in coverPoints)
                {
                    if (point.GetMagnitudeWith(player.transform.position) <
                        closestPoint.GetMagnitudeWith(player.transform.position))
                        closestPoint = point;
                }
                agent.destination = closestPoint.position;

                if (agent.remainingDistance <= distanceFromCoverPoint)
                {
                    curGunfireCount = minGunFireCount;
                    status = Status.Shoot;
                }
                
                break;

            case Status.Shoot:
                curFireDelay -= Time.deltaTime;
                if (curFireDelay <= 0)
                {
                    FireBullet();
                    curFireDelay = fireDelay;
                    curGunfireCount--;
                }

                if (!isInCoverDistence && curGunfireCount <= 0) 
                    status = Status.Find;
                break;
        }

        SyncSpriteToAgent();
    }

    void SyncSpriteToAgent()
    {
        sprite.transform.position = agentObject.transform.position;
    }

    void FireBullet()
    {
        //TODO : Get Bullet from other branch and Add it
        Debug.Log(gameObject.name + " : bullet Fired");
    }

    void ProcessStateMachine()
    {
        
    }
}
