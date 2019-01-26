using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject level;
    public GameObject swing;
    public float generateLevelTime;
    public float levelHight;

    private int levelCount = 0;

    IEnumerator Generate()
    {
        while (true)
        {
            GameObject newLevel = createLevel(levelHight, levelCount);
            GameObject balanceLevel = createLevel(-levelHight, levelCount);

            levelCount++;
            yield return new WaitForSeconds(generateLevelTime);
        }
    }

    private GameObject createLevel(float levelHight, int levelCount)
    {
        GameObject newLevel = GameObject.Instantiate(level);
        newLevel.transform.position.Set(0, levelCount * levelHight, 0);
        newLevel.transform.SetParent(swing.transform);
        newLevel.transform.rotation = Quaternion.identity;
        return newLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
