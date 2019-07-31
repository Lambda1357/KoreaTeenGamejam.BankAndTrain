using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Sprite image;
    private GameObject item;
    private SpriteRenderer sprite;
    private bool isOpen = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        int num = Random.Range(0,GameManager.instance.itemManager.itemList.Count);
        item = GameManager.instance.itemManager.itemList[num];
        GameManager.instance.itemManager.itemList.RemoveAt(num);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            sprite.sprite = image;
            GameObject temp = Instantiate(item, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
    }

}
