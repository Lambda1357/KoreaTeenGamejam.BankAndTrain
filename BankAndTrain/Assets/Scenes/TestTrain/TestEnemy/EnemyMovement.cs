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
    [SerializeField] float gunFireAimDistence;

    float curFireDelay;
    int curGunfireCount;
    float curGunFireDistence;
    CoverPoint curCoverPoint;

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
                        closestPoint.GetMagnitudeWith(player.transform.position) && !point.isUsing) 
                        closestPoint = point;
                }
                agent.destination = closestPoint.position;

                if (agent.remainingDistance <= distanceFromCoverPoint)
                {
                    curGunfireCount = minGunFireCount;
                    status = Status.Shoot;
                    curCoverPoint = closestPoint;
                }
                
                break;

            case Status.Shoot:
                curFireDelay -= Time.deltaTime;
                if (curFireDelay <= 0)
                {
                    MoveToGunFirePosition();
                    curFireDelay = fireDelay;
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

    void MoveToGunFirePosition()
    {
        Vector2 gunFirePosit;
        gunFirePosit = curCoverPoint.position;
        gunFirePosit.Set(gunFirePosit.x > 0 ? gunFirePosit.x - gunFireAimDistence : gunFirePosit.x + gunFireAimDistence, gunFirePosit.y);
        agent.destination = gunFirePosit;
        if (agent.remainingDistance <= 0.1)
            FireBullet();
    }

    void FireBullet()
    {
        //TODO : Get Bullet from other branch and Add it
        curFireDelay = fireDelay;
        curGunfireCount--;
        Debug.Log(gameObject.name + " : bullet Fired");
        ReleaseToCoverPosition();
    }

    void ReleaseToCoverPosition()
    {
        status = Status.Hide;
    }

}
