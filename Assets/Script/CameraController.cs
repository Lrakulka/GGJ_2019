using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public GameObject player;
    public float refRate = .1f;
    public float lerpRate = .1f;

    private GameObject camera;

    private void Awake()
    {
        instance = this;

        camera = this.gameObject;        
    }

    private void Start()
    {
        //StartCoroutine(UpdateCamera());
    }

    void FixedUpdate()
    {
        //while (true)
        //{
        if (LevelManager.instance.isAlive && LevelManager.instance.levelCount > 2
            && Mathf.Abs(player.transform.position.y - this.transform.position.y) > 25)
        {
            UIManager.instance.ExecuteGameEnd();
        }
        if (LevelManager.instance.levelCount > 0)
        {
            Vector3 camTarget = LevelManager.instance.levels[LevelManager.instance.levelCount-1].transform.position;
            camTarget.z = -10;
            if (Vector3.Distance(camTarget, this.transform.position) > 15)
            {
                camera.transform.position = camTarget;
            }
            else
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, camTarget, lerpRate);
            }
        }

        //    yield return new WaitForSeconds(refRate);
        //}


    }

}
