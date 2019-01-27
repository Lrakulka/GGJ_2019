using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    Rigidbody2D rigidbody;
    float h = 0;
    public float speed = 10;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        h = Random.Range(-1, 1) > 0 ? 1 : -1;
        StartCoroutine(ChangeDir());
    }

    IEnumerator ChangeDir()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            h = -h;
        }
    }

    private void FixedUpdate()
    {
       // h += Random.RandomRange(-.1f, .1f);

        rigidbody.velocity = new Vector2(h * 10, rigidbody.velocity.y);
    }

}
