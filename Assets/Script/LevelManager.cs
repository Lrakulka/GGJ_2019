﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject level;
    public GameObject balanceLevel;
    public GameObject swing;

    public SupportManager leftStopper;
    public SupportManager rightStopper;

    public float generateLevelTime;
    public float levelHight;

    [HideInInspector] public int levelCount = 0;
    [HideInInspector] public List<GameObject> levels;
    [HideInInspector] public bool isAlive = true;
    private List<GameObject> balancerLevels;

    private void Awake()
    {
        instance = this;
        levels = new List<GameObject>();
        balancerLevels = new List<GameObject>();
    }

    void Start()
    {
        StartGame();
    }

    IEnumerator Generate()
    {
        while (isAlive)
        {
            GenerateLevel();
            CleanOldLevel();
            yield return new WaitForSeconds(generateLevelTime);
        }
    }

    public void StartGame()
    {
        StartCoroutine(Generate());
        leftStopper.gameObject.SetActive(true);
        rightStopper.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        isAlive = false;
    }

    public void ResetGame()
    {
        // Destroy levels
        //if (!isAlive) return;
        isAlive = false;

        levelCount = 0;
        Destroy(levels[0].gameObject);
        Destroy(balancerLevels[0].gameObject);
        
        foreach(var l in levels)
        {
            if (l != null)
                Destroy(l.gameObject);
        }
        foreach (var l in balancerLevels)
        {
            if (l != null)
                Destroy(l.gameObject);
        }

        levels = new List<GameObject>();
        balancerLevels = new List<GameObject>();

        // reset other components
        leftStopper.ResetHealth();
        rightStopper.ResetHealth();
        swing.transform.position = new Vector3(0f, -3.6f, 0f);
        swing.transform.rotation = Quaternion.identity;

        isAlive = true;
        PlayerController.instance.Reset();
        StartGame();
    }

    private void CleanOldLevel()
    {
        if (levels.Count > 5)
        {
            Destroy(levels[levels.Count - 5].gameObject);
            Destroy(balancerLevels[balancerLevels.Count - 5].gameObject);
        }
    }

    private void GenerateLevel()
    {
        GameObject newLevel = CreateLevel(level, levelHight, levelCount, "Level" + levelCount,
                levels.Count == 0 ? null : levels[levels.Count - 1]);
        levels.Add(newLevel);
        GameObject newBalanceLevel = newLevel;
        if (balancerLevels.Count != 0)
        {
            newBalanceLevel = CreateLevel(balanceLevel, -levelHight, levelCount, "LevelBalance" + levelCount, balancerLevels[balancerLevels.Count - 1]);
        } else
        {
            newBalanceLevel = CreateLevel(balanceLevel, -levelHight, levelCount, "LevelBalance" + levelCount, levels[levels.Count - 1]);
        }
        balancerLevels.Add(newBalanceLevel);

        levelCount++;
    }

    private GameObject CreateLevel(GameObject level, float levelHight, int levelCount, string name, GameObject prevLevel)
    {
        GameObject newLevel = GameObject.Instantiate(level);
        newLevel.transform.SetParent(swing.transform);
        newLevel.transform.localRotation = Quaternion.identity;
        newLevel.name = name;
        if (prevLevel == null)
        {
            newLevel.transform.position = new Vector3(0f, 0f, 0f);
        } else
        {
            newLevel.transform.localPosition = new Vector3(prevLevel.transform.localPosition.x,
                 prevLevel.transform.localPosition.y + levelHight,
                 prevLevel.transform.localPosition.z);
        }
        return newLevel;
    }
}
