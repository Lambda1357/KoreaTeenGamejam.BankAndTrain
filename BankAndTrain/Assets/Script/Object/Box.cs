using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Sprite image;
    private GameObject item;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        int num = Random.Range(0,ItemManager.itemList.Count);
        item = ItemManager.itemList[num];
        ItemManager.itemList.RemoveAt(num);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            sprite.sprite = image;
            GameObject temp = Instantiate(item, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
    }

}
