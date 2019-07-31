using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSpriteRotation : MonoBehaviour
{
    [SerializeField] Transform dummy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform perent = transform.parent == null ? transform : transform.parent;
        transform.LookAt(dummy.position, Vector3.forward);
    }
}
