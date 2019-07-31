using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;

    public void ZoomIn(GameObject zoomObj)
    {
        Vector3 pos = zoomObj.transform.position;
        pos.z = -10;
        transform.position = pos;
        Camera.main.orthographicSize = 1.5f;
    }


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
