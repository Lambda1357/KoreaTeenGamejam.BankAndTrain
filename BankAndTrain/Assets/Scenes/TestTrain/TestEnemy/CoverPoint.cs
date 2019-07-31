using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
    public Vector2 position;
    public bool isUsing;

    private void Start()
    {
        position = transform.position;
        isUsing = false;
    }

    public float GetMagnitudeWith(Vector2 target)
    {
        return (target - position).magnitude;
    }
}
