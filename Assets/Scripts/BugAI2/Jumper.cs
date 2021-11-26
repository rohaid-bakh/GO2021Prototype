using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float m_speed = 20f;
    private float speed;
    [SerializeField] private float m_distance = .6f;
    private float distance;
    [SerializeField] private float m_jumpHeight = 150f;
    private float y_jumpHeight;
    const float k_GroundedRadius = .2f; //Needed to check the radius around the Ground Check object for ground
    private bool m_grounded = false;
    private bool m_right = true;
    private Vector2 m_rayDir = Vector2.right; //Used to detect objects infront
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_frontCheck;
    [SerializeField] private Transform m_pitCheck; //Needed to make sure bug doesn't jump over a ledge/pit
    [SerializeField] private Transform m_ownTransform; //Needed to flip bug
    [SerializeField] private LayerMask m_WhatIsGround;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        speed = m_speed;
        y_jumpHeight = m_jumpHeight;
        distance = m_distance;
        Physics2D.IgnoreLayerCollision(3, 6, true); //Needed to ignore collisions for bug/Player. ID numbers are the layer numbers in Unity
        Physics2D.IgnoreLayerCollision(6, 6, true); //Needed to ignore collisions for bug/bug.
    }

    void FixedUpdate()
    {

        distance = m_distance;
        m_grounded = false;

        //Checking for if the bug is on the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_grounded = true;

            }
        }



        // RaycastHit2D groundInfo = Physics2D.Raycast(m_groundCheck.position, Vector2.down, distance);
        // groundInfo.collider == null ||
        RaycastHit2D pitInfo = Physics2D.Raycast(m_pitCheck.position, Vector2.down, distance);

        //Flip position of frontInfo ray cast to check in front
        if (m_right)
        {
            m_rayDir = Vector2.right;
        }
        else if (!m_right)
        {
            m_rayDir = Vector2.left;
        }

        RaycastHit2D frontInfo = Physics2D.Raycast(m_frontCheck.position, m_rayDir, .4f);


        //Necessary order 
        //Need to check if a platform is about to end or if there's an obstacle. Has to check if grounded because 
        //if the bug is in midair and the groundInfo raycast sees there is no ground, it will flip into oblivion
        //pitInfo is outside the parantheses because it has a longer ray cast and is farther to the front of the bug
        //allowing for midair detection of pits
        if ((frontInfo.collider != null && m_grounded && frontInfo.collider.name != "MC" && frontInfo.collider.gameObject.layer != 6) || (pitInfo.collider == null && m_grounded))
        {
            Flip();
        }

        Move(m_speed * Time.fixedDeltaTime, m_right, m_grounded);

    }

    public void Move(float movement, bool direction, bool grounded)
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



        m_Rigidbody2D.velocity = new Vector2(movement * m_speed * vectordirection, m_Rigidbody2D.velocity.y);

        //Makes the bug hop everytime it's on the ground.
        if (grounded)
        {
            m_Rigidbody2D.AddForce(new Vector2(3f, y_jumpHeight));
        }

        y_jumpHeight = y_jumpHeight;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_right = !m_right;
        Vector3 theScale = m_ownTransform.localScale;
        theScale.x *= -1;
        m_ownTransform.localScale = theScale;
    }


    // UNCOMMENT TO DEBUG
    // USED TO SEE THE VISUAL RADIUS OF THE GROUNDCHECK RAYCAST

    void OnDrawGizmosSelected()
    {
        if (m_groundCheck == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(m_groundCheck.position, k_GroundedRadius);
        Debug.DrawRay(m_pitCheck.position, Vector2.down, Color.red, Time.deltaTime, true);

    }

}
