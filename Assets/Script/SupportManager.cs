using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportManager : MonoBehaviour
{
    public static SupportManager instance;
    public GameObject resetGameButton;
    public UnityEngine.UI.Text levelText;
    public UnityEngine.UI.Text healthLabel;

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
        healthLabel.text = currHealth.ToString();
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
        healthLabel.text = currHealth.ToString();
        if (currHealth == 0)
        {
            AkSoundEngine.StopAll();// StopPlayingID("Play_Music");
            AkSoundEngine.PostEvent("Play_END", this.gameObject);
            LevelManager.instance.StopGame();
            levelText.text = LevelManager.instance.levelCount.ToString();
            levelText.gameObject.SetActive(true);
            resetGameButton.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
