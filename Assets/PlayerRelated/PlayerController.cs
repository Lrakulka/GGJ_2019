using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public static PlayerController instance;

    private Vector3 startPosition;
    private List<GameObject> used = new List<GameObject>();

    public float speed = 10;

    private int currentLevel = 0;

    private void Awake()
    {
        instance = this;
        startPosition = this.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        currentLevel = 0;
        this.transform.position = startPosition;
        carry = null;
        for (int i = used.Count - 1; i >= 0; i--)
            Destroy(used[i]);
        used.Clear();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h != 0)
            AkSoundEngine.PostEvent("Play_Footsteps", this.gameObject);

        rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!LevelManager.instance.isAlive)
            {
                LevelManager.instance.ResetGame();
                return;
            }

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

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        hits.AddRange(Physics2D.RaycastAll(startPos, Vector3.forward, 100));
        hits.AddRange(Physics2D.RaycastAll(startPos + Vector3.down, Vector3.forward, 100));
        hits.AddRange(Physics2D.RaycastAll(startPos + Vector3.up, Vector3.forward, 100));
        hits.AddRange(Physics2D.RaycastAll(startPos + Vector3.up * 2, Vector3.forward, 100));
        hits.AddRange(Physics2D.RaycastAll(startPos + Vector3.up * 3, Vector3.forward, 100));
        hits.AddRange(Physics2D.RaycastAll(startPos + Vector3.down  * 1.5f, Vector3.forward, 100));

        for (int i = hits.Count - 1; i>=0; i--)
        {
            if (hits[i].transform.name == "Door")
            {
                EnterDoor();
                return;
            } else if (hits[i].transform.name == this.transform.name)
            {
                hits.RemoveAt(i);
            } else if (hits[i].transform.tag == "Ignore")
            {
                hits.RemoveAt(i);
            }
        }

        if (hits.Count > 0)
            foreach (var hit in hits)
                if (hit.transform.gameObject != this.gameObject)
                {
                    Debug.Log("Picking " + hit.transform.name);
                    carry = hit.transform.gameObject;
                    used.Add(carry);
                    hit.transform.SetParent(this.transform);
                    hit.transform.position = hit.transform.position + Vector3.up;
                    hit.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    return;
                }
    }

    private void Drop(float speed)
    {
        //TODO: drop object
        Debug.Log("Dropping.");

        Rigidbody2D rigidbodyReference = carry.transform.GetComponent<Rigidbody2D>();

        rigidbodyReference.bodyType = RigidbodyType2D.Dynamic;
        rigidbodyReference.velocity = new Vector2(speed * 500 / rigidbodyReference.mass, 300 / rigidbodyReference.mass);

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
