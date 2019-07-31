﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public DataManager dataManager;
    public CameraManager cameraManager;
    public ItemManager itemManager;
    public SceneStateManager sceneState;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        dataManager = GetComponent<DataManager>();
        cameraManager = GetComponent<CameraManager>();
        itemManager = GetComponent<ItemManager>();
        sceneState = GetComponent<SceneStateManager>();
    }

}
