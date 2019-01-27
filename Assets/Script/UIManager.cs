using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject resetGameButton;
    public UnityEngine.UI.Text levelText;

    private void Awake()
    {
        instance = this;
    }

    public void ExecuteGameEnd()
    {
        LevelManager.instance.StopGame();
        levelText.text = LevelManager.instance.levelCount.ToString();
        levelText.gameObject.SetActive(true);
        resetGameButton.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
