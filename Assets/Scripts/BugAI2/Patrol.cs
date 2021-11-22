using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    private float speed;
    private bool m_right = true;
    private Vector2 m_rayDir = Vector2.right; //Used to detect objects infront
    [SerializeField] private float m_distance;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_ownTransform;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        speed = m_speed;
        m_distance = 1f;
        Physics2D.IgnoreLayerCollision(3,6, true); //Needed to ignore collisions for bug/Player. ID numbers are the layer numbers in Unity
    }

    void FixedUpdate()
    {
        Move(speed * Time.fixedDeltaTime, m_right);
        RaycastHit2D groundInfo = Physics2D.Raycast(m_groundCheck.position, Vector2.down, m_distance);


        
        //Flip position of frontInfo ray cast to check in front
        if (m_right)
        {
            m_rayDir = Vector2.right;
        }
        else if (!m_right)
        {
            m_rayDir = Vector2.left;
        }
        RaycastHit2D frontInfo = Physics2D.Raycast(m_groundCheck.position, m_rayDir, m_distance);


        //Checks to see if there's no ground in front of the bug, or if there's a collision
        if (groundInfo.collider == null || (frontInfo.collider != null && frontInfo.collider.name != "MC"))
        {
            Flip();
        }

    }

    public void Move(float movement, bool direction)
    {
        //Necessary code block to make sure that the bug is moving in the right direction.
        int vectordirection = 0;
        if (direction)
        {
            vectordirection = 1;
        }
        else
        {
            vectordirection = -1;
        }

        m_Rigidbody2D.velocity = new Vector2(movement * speed * vectordirection, m_Rigidbody2D.velocity.y);

    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_right = !m_right;
        Vector3 theScale = m_ownTransform.localScale;
        theScale.x *= -1;
        m_ownTransform.localScale = theScale;
    }

}
