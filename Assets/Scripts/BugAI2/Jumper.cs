using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float m_speed;
    private bool m_right = true;
    private Vector2 m_rayDir = Vector2.right;
    [SerializeField] private float m_distance;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_ownTransform;
    [SerializeField] private Transform m_pitCheck;
    [SerializeField] private Transform m_MC;
    [SerializeField] private float m_jumpHeight;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_grounded = false;
    const float k_GroundedRadius = .4f;
    [SerializeField] private LayerMask m_WhatIsGround;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_speed = 20f;
        m_distance = 1f;
        m_jumpHeight = 150f;
        Physics2D.IgnoreLayerCollision(3,6, true);
    }

    void FixedUpdate()
    {
        m_grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_grounded = true;
				
			}
		}

        Debug.Log("Is Grounded?: "+ m_grounded);

        Move(m_speed * Time.fixedDeltaTime, m_right, m_grounded);
        RaycastHit2D groundInfo = Physics2D.Raycast(m_groundCheck.position, Vector2.down, m_distance);
        RaycastHit2D pitInfo = Physics2D.Raycast(m_pitCheck.position, Vector2.down, 1.3f);

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
        if (((groundInfo.collider == null || frontInfo.collider != null) && m_grounded) || pitInfo.collider == null)
        {
            Flip();
        }

       

    }

    public void Move(float movement, bool direction, bool grounded)
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
        if (grounded) {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_jumpHeight));
        }

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

    
    void OnDrawGizmosSelected(){
            if (m_groundCheck == null) {
                return;
            }
            Gizmos.DrawWireSphere(m_groundCheck.position, k_GroundedRadius);
            
    }

}
