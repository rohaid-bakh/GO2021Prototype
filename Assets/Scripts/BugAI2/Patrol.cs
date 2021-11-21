using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float m_speed;
    private bool m_right = true;
    private Vector2 m_rayDir = Vector2.right;
    [SerializeField] private float m_distance;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_ownTransform;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_speed = 20f;
        m_distance = 1f;
        Physics2D.IgnoreLayerCollision(3,6, true);
    }

    void FixedUpdate()
    {
        Move(m_speed * Time.fixedDeltaTime, m_right);
        RaycastHit2D groundInfo = Physics2D.Raycast(m_groundCheck.position, Vector2.down, m_distance);

        if (m_right)
        {
            m_rayDir = Vector2.right;
        }
        else if (!m_right)
        {
            m_rayDir = Vector2.left;
        }

        RaycastHit2D frontInfo = Physics2D.Raycast(m_groundCheck.position, m_rayDir, m_distance);


        //checks to see if there's no ground in front of the bug, or if there's a collision
        if (groundInfo.collider == null || frontInfo.collider != null)
        {
            Flip();
        }

    }

    public void Move(float movement, bool direction)
    {
        int vectordirection = 0;
        if (direction)
        {
            vectordirection = 1;
        }
        else
        {
            vectordirection = -1;
        }

        m_Rigidbody2D.velocity = new Vector2(movement * m_speed * vectordirection, m_Rigidbody2D.velocity.y);

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_right = !m_right;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = m_ownTransform.localScale;
        theScale.x *= -1;
        m_ownTransform.localScale = theScale;
    }

}
