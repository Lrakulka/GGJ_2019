using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public Animator anim;
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

    public bool m_FacingRight;

    private void FixedUpdate()
    {
        // h += Random.RandomRange(-.1f, .1f);

        rigidbody.velocity = new Vector2(h * 10, rigidbody.velocity.y);

        anim.SetFloat("Start", Mathf.Abs(h));

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

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            transform.Rotate(0f, 180f, 0f);
        }

}
