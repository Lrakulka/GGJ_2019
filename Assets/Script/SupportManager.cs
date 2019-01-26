using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportManager : MonoBehaviour
{
    public static SupportManager instance;

    public int health;
    private float prevTime;
    private int currHealth;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (!LevelManager.instance.isAlive || Time.realtimeSinceStartup - prevTime < 1f)
        {
            return;            
        }
        prevTime = Time.realtimeSinceStartup;
        currHealth--;
        if (currHealth == 0)
        {
           
            LevelManager.instance.ResetGame();
            
           // LevelManager.instance.StopGame();
        }
        Debug.Log(currHealth);
    }
}
