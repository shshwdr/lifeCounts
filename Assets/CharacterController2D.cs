using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsJumpDir;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsFakeGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	bool m_Grounded;            // Whether or not the player is grounded.
	bool m_FakeGrounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	float jumpCoolDownTimer = 1;
	public float jumpCoolDown = 0.4f;
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	AudioSource audioSource;
	public AudioClip[] landClips;
	public AudioClip[] jumpClips;
	public AudioClip dialogueClip;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
	
	public Vector2 jumpDir;


	float originDrag;
	float originMass;
	float dragModifier;
	float massModifier;
    public void UpdateDragModifier(float multiplier)
    {
		dragModifier = multiplier;
		m_Rigidbody2D.drag = originDrag * dragModifier;

	}

	public void UpdateMassModifier(float multiplier)
	{
		massModifier = multiplier;
		m_Rigidbody2D.mass = originMass * massModifier;

	}

	//void OnCollisionEnter2D(Collision2D coll)
	//{
	//    // If a missile hits this object
	//    if (coll.transform.tag == "ground"|| coll.transform.tag == "NPC")
	//    {
	//        //Debug.Log("HIT!");

	//        // Spawn an explosion at each point of contact
	//        foreach (ContactPoint2D missileHit in coll.contacts)
	//        {
	//            Vector2 hitPoint = missileHit.point;
	//            jumpDir = (Vector2)transform.position - hitPoint;
	//            jumpDir.Normalize();
	//            //Instantiate(explosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
	//        }
	//    }
	//}

	//private void OnCollisionExit2D(Collision2D coll)
	//{
	//	if (coll.transform.tag == "ground" || coll.transform.tag == "NPC")
	//	{
	//		jumpDir = Vector2.zero;
	//	}
	//}

	void playLandClip()
    {

		//audioSource.clip =  landClips[Random.Range(0,landClips.Length)];
		//audioSource.pitch = Random.Range(0.5f, 1f);
		//audioSource.volume = Random.Range(0.5f, 1.5f);
		audioSource.PlayOneShot(landClips[Random.Range(0, landClips.Length)], Random.Range(0.8f, 1f));
	}

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
		originMass = m_Rigidbody2D.mass;
		originDrag = m_Rigidbody2D.drag;
	}

	private void FixedUpdate()
	{
		jumpCoolDownTimer += Time.deltaTime;
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundCheck.GetComponent<CircleCollider2D>().radius * transform.localScale.x +0.1f, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				//if (colliders[i].tag == "ground")
				//{
				//	if ((colliders[i].transform.position - transform.position).y < 0)
				//	{
				//		continue;
				//	}
				//}
				m_Grounded = true;
				if (!wasGrounded)
                {
					playLandClip();
                    //OnLandEvent.Invoke();

                }
            }
		}

		//colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundCheck.GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f, m_WhatIsJumpDir);
		//float bestDot = 0;
		//for (int i = 0; i < colliders.Length; i++)
		//{
		//	if (colliders[i].gameObject != gameObject)
		//	{
		//		var dir = (transform.position - colliders[i].transform.position).normalized;
		//		var dot = Vector3.Dot(dir, transform.position);
		//		if (dot < 0 && (jumpDir == Vector2.zero || dot< bestDot))
  //              {
		//			jumpDir = (colliders[i].transform.position - transform.position).normalized;
		//			bestDot = dot;

		//		}
		//	}
		//}

		colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundCheck.GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.1f, m_WhatIsFakeGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				if (colliders[i].tag == "ground")
				{
					if ((colliders[i].transform.position - transform.position).y < 0)
					{
						continue;
					}
				}
				m_FakeGrounded = true;
                if (colliders[i].GetComponent<CharacterController2D>())
                {
                    if (colliders[i].GetComponent<CharacterController2D>().m_Grounded)
                    {
 						m_Grounded = true;
                    }
                }
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

  //  private void OnCollisionEnter2D(Collision2D collision)
  //  {
  //      if (m_Rigidbody2D.velocity.magnitude > 3 || (collision.collider.GetComponent<Rigidbody2D>() && collision.collider.GetComponent<Rigidbody2D>().velocity.magnitude>3))
  //      {
		//	audioSource.clip = landClip;
		//	audioSource.Play();
		//}
  //  }


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
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
			}
			else
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

			// Move the character by finding the target velocity
			//Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			//m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			m_Rigidbody2D.AddForce(new Vector2(m_JumpForce* move, 0));
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
		if (m_Grounded && jump && jumpCoolDownTimer>=jumpCoolDown)
		{
			// Add a vertical force to the player.
			//m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			//m_Rigidbody2D.AddForce((Vector2.zero - (Vector2)transform.position).normalized * m_JumpForce/*new Vector2(0f, m_JumpForce)*/);
			jumpDir = Vector2.zero;
			jumpCoolDownTimer = 0;
            if (jumpClips.Length>0)
            {

				audioSource.PlayOneShot(jumpClips[Random.Range(0,jumpClips.Length)],Random.Range(0.7f, 0.8f));
			}
		}
	}

	public void playDialogueSound()
    {
		audioSource.PlayOneShot(dialogueClip);
    }


	private void Flip()
	{
		return;
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}