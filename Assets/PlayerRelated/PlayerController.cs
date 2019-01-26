using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float speed = 10;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(h * speed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Action!");
        }
    }

}
