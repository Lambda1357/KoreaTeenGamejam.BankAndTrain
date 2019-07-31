using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUI : MonoBehaviour
{
    public Sprite[] image;
    public FadeUI fade;
    public GameObject quest;
    private SpriteRenderer my;
    private float animationSpeed;

    void Start()
    {
        my = GetComponent<SpriteRenderer>();
        animationSpeed = 0.08f;
    }

    public void StartButton()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        for (int i=0; i < image.Length; i++)
        {
            my.sprite = image[i];
            yield return new WaitForSeconds(animationSpeed);
        }
        fade.Alpha(1);
        CameraManager cam = Camera.main.GetComponent<CameraManager>();
        cam.ZoomIn(quest);
    }
}
