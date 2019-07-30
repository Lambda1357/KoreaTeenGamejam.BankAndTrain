using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
