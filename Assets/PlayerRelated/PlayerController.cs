using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Vector3 startPosition;

    public float speed = 10;

    private int currentLevel = 0;

    private void Awake()
    {
        startPosition = this.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(h * speed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (carry == null)
                Pickup();
            else
                Drop(h);
        }
    }

    private GameObject carry = null;

    private void Pickup()
    {
        Vector3 startPos = this.transform.position + Vector3.forward;

        //Debug.DrawRay(startPos, Vector3.forward * 100, Color.red, 100);
        //Debug.DrawRay(startPos + Vector3.down, Vector3.forward * 100, Color.red, 100);
        //Debug.DrawRay(startPos + Vector3.down * 1.5f, Vector3.forward * 100, Color.red, 100);
        //Debug.DrawRay(startPos + Vector3.up, Vector3.forward * 100, Color.red, 100);
        //Debug.DrawRay(startPos + Vector3.up * 2, Vector3.forward * 100, Color.red, 100);
        //Debug.DrawRay(startPos + Vector3.up * 3, Vector3.forward * 100, Color.red, 100);

        RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, Vector3.forward, 100);
        if (hits.Length <= 1)
            hits = Physics2D.RaycastAll(startPos + Vector3.down, Vector3.forward, 100);
        if (hits.Length <= 1)
            hits = Physics2D.RaycastAll(startPos + Vector3.up, Vector3.forward, 100);
        if (hits.Length <= 1)
            hits = Physics2D.RaycastAll(startPos + Vector3.up * 2, Vector3.forward, 100);
        if (hits.Length <= 1)
            hits = Physics2D.RaycastAll(startPos + Vector3.up * 3, Vector3.forward, 100);
        if (hits.Length <= 1)
            hits = Physics2D.RaycastAll(startPos + Vector3.down  * 1.5f, Vector3.forward, 100);

        if (hits.Length > 0)
            foreach (var hit in hits)
            if (hit.transform.gameObject != this.gameObject)
            {
                if (hit.transform.name == "Door")
                {
                    EnterDoor();
                    return;
                }
                
                carry = hit.transform.gameObject;
                hit.transform.SetParent(this.transform);
                hit.transform.position = hit.transform.position + Vector3.up;
                hit.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
    }

    private void Drop(float speed)
    {
        //TODO: drop object
        Debug.Log("Dropping.");

        Rigidbody2D rigidbodyReference = carry.transform.GetComponent<Rigidbody2D>();

        rigidbodyReference.bodyType = RigidbodyType2D.Dynamic;
        rigidbodyReference.velocity = new Vector2(speed * 500 / rigidbodyReference.mass, 0);

        carry.transform.SetParent(null);
        carry = null;
    }

    private void EnterDoor()
    {
        if (LevelManager.instance.levels.Count == currentLevel + 1)
            return;

        Debug.Log("Entering door!");
        currentLevel++;
        this.transform.position = LevelManager.instance.levels[currentLevel].transform.position;
    }

}
