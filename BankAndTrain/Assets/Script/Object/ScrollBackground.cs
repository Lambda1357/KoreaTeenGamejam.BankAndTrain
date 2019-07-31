using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public Transform[] background;
    Transform kamera;

    void Awake()
    {
        kamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Vector3 kamera_position = (Vector2)kamera.transform.position;
		
		float y = -kamera_position.y;

		transform.position = kamera_position + new Vector3(0,y % 10f,transform.position.z);

		background[0].localPosition = new Vector3(0,0 ,0);
		background[1].localPosition = new Vector3(0,10f,0);
        background[2].localPosition = new Vector3(0,-10f,0);
    }
}
