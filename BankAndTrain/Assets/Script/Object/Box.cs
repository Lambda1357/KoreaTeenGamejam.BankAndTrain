using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public FadeUI fade;
    public GameObject popUp, box;
    public Image questItem;
    public Text text;
    public Sprite image;
    public GameObject item;
    private SpriteRenderer sprite;
    public bool isOpen = false;
    public bool isEnd = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(!isEnd)
        {
            int num = Random.Range(0,GameManager.instance.itemManager.itemList.Count);
            item = GameManager.instance.itemManager.itemList[num];
            GameManager.instance.itemManager.itemList.RemoveAt(num);
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.0f);
        fade.Alpha(1.0f);
        popUp.SetActive(true);
        questItem.sprite = GameManager.instance.itemManager.endItem[GameManager.instance.questNum];
        text.text = "" + GameManager.instance.itemManager.moneyList[GameManager.instance.questNum];
        GameManager.instance.money += GameManager.instance.itemManager.moneyList[GameManager.instance.questNum];
        PlayerPrefs.SetInt("Money", GameManager.instance.money);
        GameManager.instance.itemManager.Set();
        box.SetActive(false);
    }

    public void Open()
    {
        isOpen = true;
        sprite.sprite = image;
        GameObject temp = Instantiate(item, transform.position + Vector3.up * 3.0f, Quaternion.identity);
        SpriteRenderer sp = temp.GetComponent<SpriteRenderer>();
        sp.sprite = GameManager.instance.itemManager.endItem[GameManager.instance.questNum];
        GameManager.instance.dataManager.dataList[GameManager.instance.questNum].clear = true;
        GameManager.instance.dataManager.SaveData(GameManager.instance.dataManager.dataList);
        StartCoroutine(EndGame());
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
