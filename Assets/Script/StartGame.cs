using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
      // Update is called once per frame
    public void StartTheGame (string NewScene)
    {
        SceneManager.LoadScene(NewScene, LoadSceneMode.Single);
    }
}
