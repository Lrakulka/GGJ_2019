using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool m_FacingRight = true;
    private Rigidbody2D rigidbody;
    public static PlayerController instance;
    public Animator anim;
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
        rigidbody.velocity = new Vector2(0, 0);
        this.transform.position = startPosition;
        carry = null;
        for (int i = used.Count - 1; i >= 0; i--)
            Destroy(used[i]);
        used.Clear();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Start", Mathf.Abs(h)*10);
        if (h != 0)
            AkSoundEngine.PostEvent("Play_Footsteps", this.gameObject);

        rigidbody.velocity = new Vector2(h * speed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
           /* if (!LevelManager.instance.isAlive)
            {
                LevelManager.instance.ResetGame();
                return;
            }*/

            if (carry == null)
                Pickup();
            else
                Drop(h);
        }
        Debug.Log(h);
        if (h > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
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
            if (hits[i].transform.tag == "NPC")
            {
                Eat(hits[i].transform.gameObject);
                return;
            } else if (hits[i].transform.name == "Door")
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

    private void Eat(GameObject food)
    {
        LevelManager.instance.addHPToSupport(1);
        Destroy(food);
    }

    private void Drop(float speed)
    {
        //TODO: drop object
        Debug.Log("Dropping.");
        
       // AkSoundEngine.PostEvent("Play_Drop", gameObject);
       // AkSoundEngine.PostEvent("Play_Footsteps", this.gameObject);

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

        Transform a = LevelManager.instance.levels[currentLevel].transform.Find("Shuffleable");

       // Debug.Log(a.name);

        a = a.Find("DoorX");

        // Debug.Log(a.name);

        Vector3 newPos = a.transform.position + Vector3.up * 2;

        this.transform.position = newPos;
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

}
