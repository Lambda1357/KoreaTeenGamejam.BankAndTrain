using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
    public Vector3 position;
    public bool isUsing;

    private void Start()
    {
        position = transform.position;
        isUsing = false;
    }

    public float GetMagnitudeWith(Vector3 target)
    {
        return (target - position).magnitude;
    }
}
