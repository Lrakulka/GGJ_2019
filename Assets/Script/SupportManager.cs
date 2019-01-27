using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportManager : MonoBehaviour
{
    public UnityEngine.UI.Text healthLabel;

    public int health;
    private float prevTime;
    [HideInInspector] public int currHealth;

    private void Awake()
    {
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

            this.gameObject.SetActive(false);
            UIManager.instance.ExecuteGameEnd();
        }
    }
}
