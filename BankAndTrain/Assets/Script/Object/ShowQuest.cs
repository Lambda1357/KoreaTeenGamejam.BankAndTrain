using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQuest : MonoBehaviour
{
    public Sprite[] images;
    private SpriteRenderer sprite;
    private List<int> random = new List<int>();
    private int human = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        for(int i = 0; i < 6; i++)
        {
            random.Add(i);
        }

        for(int i = 0; i < GameManager.instance.dataManager.dataList.Count; i++)
        {
            if(GameManager.instance.dataManager.dataList[i].clear)
            {
                random.RemoveAt(GameManager.instance.dataManager.dataList[i].num);
            }
        }

        if(random.Count == 0)
        {
            Debug.Log("check boss");
            sprite.sprite = images[6];
            GameManager.instance.sceneState.index = 5;
            return;
        }

        human = Random.Range(0, random.Count - 1);
        sprite.sprite = images[random[human]];
        GameManager.instance.questNum = random[human];
    }
    
}
