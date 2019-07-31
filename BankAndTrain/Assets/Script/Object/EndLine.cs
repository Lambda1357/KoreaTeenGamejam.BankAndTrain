using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public bool isLast;
    public SceneStateManager stateManager;
    private RoomState roomState;

    void Start()
    {
        stateManager = GameManager.instance.GetComponent<SceneStateManager>();
        roomState = GameObject.Find("Boxes").GetComponent<RoomState>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.CompareTag("Player"))
        {
            if(isLast)
            {
                if(GameManager.instance.isKey)
                {
                    stateManager.LoadNextLevel();
                }
            }
            else
            {
                if(GameManager.instance.isKey)
                {
                    stateManager.LoadNextLevel();
                }
                else
                {
                    if(roomState.Reserch())
                    {
                        stateManager.LoadNextLevel();
                    }
                }   
            }
        }
    }
}
