using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public static GenerateLevel instance;

    public GameObject level;
    public GameObject balanceLevel;
    public GameObject swing;
    public float generateLevelTime;
    public float levelHight;

    [HideInInspector] public int levelCount = 0;
    [HideInInspector] public List<GameObject> levels;
    private List<GameObject> balancerLevels;

    private void Awake()
    {
        instance = this;
        levels = new List<GameObject>();
        balancerLevels = new List<GameObject>();
    }

    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            GameObject newLevel = createLevel(level, levelHight, levelCount, "Level" + levelCount,
                levels.Count == 0 ? null : levels[levels.Count - 1]);
            GameObject newBalanceLevel = newLevel;
            if (balancerLevels.Count != 0)
            {
                newBalanceLevel = createLevel(balanceLevel, -levelHight, levelCount, "LevelBalance" + levelCount, balancerLevels[balancerLevels.Count - 1]);
            }
            levels.Add(newLevel);
            balancerLevels.Add(newBalanceLevel);
            levelCount++;
            yield return new WaitForSeconds(generateLevelTime);
        }
    }

    private GameObject createLevel(GameObject level, float levelHight, int levelCount, string name, GameObject prevLevel)
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
