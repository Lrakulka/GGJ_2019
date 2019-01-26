using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject level;
    public GameObject balanceLevel;
    public GameObject swing;
    public float generateLevelTime;
    public float levelHight;

    private int levelCount = 0;
    private List<GameObject> levels;

    IEnumerator Generate()
    {
        while (true)
        {
            GameObject newLevel = createLevel(level, levelHight, levelCount, "Level" + levelCount, 
                levels.Count == 0 ? null: levels[levels.Count - 1]);
            //GameObject newBalanceLevel = createLevel(balanceLevel, -levelHight, levelCount, "LevelBalance" + levelCount);
            levels.Add(newLevel);
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

    // Start is called before the first frame update
    void Start()
    {
        levels = new List<GameObject>();
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
