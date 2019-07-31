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
    public GameData(int n,bool c)
    {
		num = n;
        clear = c;
    }
}

public class DataManager : MonoBehaviour
{
    private List<GameData> datas = new List<GameData>();
    public  List<GameData> dataList = new List<GameData>();

    void Start()
    {
        // data not null
		if (File.Exists(Application.persistentDataPath + "/Data.json"))
		{
			StartCoroutine(LoadData());
		}
		// data null
		else
		{
			for(int i = 0; i  < 6; i++)
			{
				datas.Add(new GameData(i, false));
			}
			SaveData(datas);
		}
    }

    public void SaveData(List<GameData> data)
	{
		// data save to json
		JsonData gamemData = JsonMapper.ToJson(data);

		// create json file
		File.WriteAllText(Application.persistentDataPath + "/Data.json",gamemData.ToString());
	}

    IEnumerator LoadData()
    {
        // load json file
		string JsonString = File.ReadAllText(Application.persistentDataPath + "/Data.json");
		// txt to object
		JsonData gamemData = JsonMapper.ToObject(JsonString);

        Parsing(gamemData);

        yield return null;

    }

	public void Parsing(JsonData data)
	{	
        // load to array
		for(int i = 0; i <data.Count; i++)
		{
            dataList.Add( new GameData(int.Parse(data[i]["num"].ToString()),bool.Parse(data[i]["clear"].ToString()) ));
		}    	
    }

}
