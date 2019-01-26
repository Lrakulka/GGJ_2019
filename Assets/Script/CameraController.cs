using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

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
            if (GenerateLevel.instance.levels.Count > 0)
            {
                Vector3 camTarget = GenerateLevel.instance.levels[GenerateLevel.instance.levelCount-1].transform.position;
                camTarget.z = -10;

                camera.transform.position = Vector3.Lerp(camera.transform.position, camTarget, lerpRate);
            }

        //    yield return new WaitForSeconds(refRate);
        //}


    }

}
