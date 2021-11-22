
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Range (10,50)] [SerializeField] private float m_walkSpeed = 20f;			// How fast the character walks. Testing purposes only. Should be set to 20 by default
	[SerializeField] private float m_yJumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private float m_XJumpForce = 3f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;	
	
    public Transform catchCheck;
    public LayerMask bugLayer;						// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;
					// A collider that will be disabled when crouching
    public float catchRange = 1f;
    public Animator animator;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Run; //Whether or not the player is running
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	private bool m_wasRunning = false; // For changing movement smoothing during jumping

	
	
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool run, bool catching)
	{
		// If crouching, check to see if the character can stand up
		if (catching) {
			Catching();
		}
		if (m_wasCrouching)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			//changes the walkSpeed velocity depending if the user is running or not
			//makes sure to only enable running while grounded and not crouching

			if (run && m_Grounded && !crouch) {
				m_walkSpeed = 18f;
				if (!m_wasRunning) {
					m_wasRunning = true;
				}

			} else  {
				m_walkSpeed = 10 ;
				// responsible for setting smoothing setting to only change while running/jumping
				if (m_wasRunning && !m_Grounded) {
					m_MovementSmoothing = 0.0f;

				} else {
					m_wasRunning = false;
					m_MovementSmoothing = 0.0f;
				}
			}
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * m_walkSpeed, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;

			// Changes the X and Y velocity depending on if the player was running or walking before jumping
			if (m_wasRunning) {
				m_yJumpForce = 900f;
				m_XJumpForce = 500f;
				
			} else {
				m_yJumpForce = 700f;
				m_XJumpForce = 0f;
			}

			m_Rigidbody2D.AddForce(new Vector2(m_XJumpForce, m_yJumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void Catching()
    {
        animator.SetTrigger("Catch");
        Collider2D[] bugs = Physics2D.OverlapCircleAll(catchCheck.position, catchRange, bugLayer);

        foreach(Collider2D bug in bugs) {
            bug.GetComponent<Caught>().Catch(bug);
        }

    }

	
}
