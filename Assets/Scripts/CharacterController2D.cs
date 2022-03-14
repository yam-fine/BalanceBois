using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] AudioSource ass;
	[SerializeField] List<AudioClip> landingSounds, jumpingSounds;
	[SerializeField] GameObject landParticles;

	public float JumpForce { get { return m_JumpForce; } set { m_JumpForce = value; } }

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded = false, lastGrounded = false;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	Animator anim;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}


	private void FixedUpdate()
	{		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

		anim.SetBool("IsGrounded", m_Grounded);

		if (m_Grounded && lastGrounded != m_Grounded) {
			Landing();
        }
		else if (!m_Grounded && lastGrounded != m_Grounded) {
			Jumping();
        }
		lastGrounded = m_Grounded;
	}

	void Landing() {
		ass.clip = landingSounds[Random.Range(0, landingSounds.Count)];
		ass.Play();
		Instantiate(landParticles, m_GroundCheck.position, Quaternion.identity);
	}

	void Jumping() {
		ass.clip = jumpingSounds[Random.Range(0, jumpingSounds.Count)];
		ass.Play();
	}


	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

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
			anim.SetBool("IsGrounded", false);
			m_Grounded = false;
			lastGrounded = m_Grounded;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
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
}