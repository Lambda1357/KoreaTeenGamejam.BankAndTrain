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

    
    [Range(0.0f, 1.0f), SerializeField]
    float takeCloseCoverRate;
    [Range(0.0f, 1.0f), SerializeField]
    float directAttackRate;
    [SerializeField] float fireDelay;
    [SerializeField] float distanceFromCoverPoint;
    [SerializeField] int minGunFireCount;
    [SerializeField] float gunFireAimDistence;
    [SerializeField] float coverZDistence;

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
        switch (status)
        {
            case Status.Find:
                float takeActionValue;
                takeActionValue = Random.Range(0.0f, 1.0f);
                if (takeActionValue <= takeCloseCoverRate)
                    status = Status.Hide;
                else takeActionValue = Random.Range(0.0f, 1.0f);
                if (takeActionValue <= directAttackRate)
                    status = Status.Shoot;
                break;

            case Status.Hide:
                CoverPoint closeistPoint = coverPoints[0];
                foreach (var point in coverPoints)
                {
                    if ((point.position - player.transform.position).magnitude < closeistPoint.GetMagnitudeWith(player.transform.position))
                        closeistPoint = point;
                }

                Vector3 pos = closeistPoint.position;

                float zOffset = player.transform.position.z > agentObject.transform.position.z ? -coverZDistence : coverZDistence;

                agent.destination = new Vector3(pos.x, pos.y, pos.z + zOffset);

                if (agent.remainingDistance < distanceFromCoverPoint)
                {
                    curGunfireCount = minGunFireCount;
                    status = Status.Shoot;
                }
                break;

            case Status.Shoot:
                curFireDelay += Time.deltaTime;

                agent.destination = player.transform.position;
                if(curFireDelay >= fireDelay)
                {
                    //TODO: fire
                    Debug.Log("Fire!!");
                    curGunfireCount--;
                    curFireDelay = 0;
                }

                if (curGunfireCount <= 0)
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
