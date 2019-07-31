using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public SceneStateManager stateManager;

    void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.CompareTag("Player"))
        {
            stateManager.LoadNextLevel();
        }
    }
}
