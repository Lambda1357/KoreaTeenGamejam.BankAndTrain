﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;


    void Update()
    {
        if(player != null)
        {
            Vector3 pos = player.transform.position;
            pos.x = 0;
            pos.z = -10;
            transform.position = pos;
        }
    }
}
