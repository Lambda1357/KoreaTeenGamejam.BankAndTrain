using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

[SerializeField]
public class GameData
{
    public int num;
	public bool clear;
    public GameData(int n , bool c)
    {
        num = n;
        clear = c;
    }
}

public class DataManager : MonoBehaviour
{
    private List<GameData> datas = new List<GameData>();

    void Start()
    {
        // data not null
		if (File.Exists(Application.persistentDataPath + "/Data.json"))
		{
			LoadData();
		}
		// data null
		else
		{
			for(int i = 0; i  < 6; i++)
			{
				datas.Add(new GameData(i,false));
			}
			SaveData();
		}
    }

    public void SaveData()
	{
		// data save to json
		JsonData gamemData = JsonMapper.ToJson(datas);

		// create json file
		File.WriteAllText(Application.persistentDataPath + "/Data.json",gamemData.ToString());
	}

	public void LoadData()
	{
		// load json file
		string JsonString = File.ReadAllText(Application.persistentDataPath + "/Data.json");

		// txt to object
		JsonData gamemData = JsonMapper.ToObject(JsonString);

        List<GameData> dataList = new List<GameData>();
		// load to array
		for(int i = 0; i <gamemData.Count; i++)
		{
			//dataList.Add(datas[i]);
            //Debug.Log();
		}    	
    }

}
