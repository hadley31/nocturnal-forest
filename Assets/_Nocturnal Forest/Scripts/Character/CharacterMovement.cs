using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterBase
{
	#region Exposed Fields

	[SerializeField] private float m_MoveSpeed = 10f;
	[SerializeField] private float m_JumpForce = 550f;
	[SerializeField] private float m_JumpCounterForce = 330f;
	[SerializeField] private float m_JumpFinishedCounterForce = 500f;
	[SerializeField] private float m_JumpDuration = 1.0f;
	[SerializeField] private float m_DashDistance = 5.0f;
	[SerializeField] private GameObject poof;
	[SerializeField] [Range (0, 1)] private float m_CrouchSpeed = 0.25f;
	[SerializeField] private LayerMask m_WhatIsGround;

	#endregion

	#region Hidden Fields

	private Transform m_GroundCheck;
	const float k_GroundedRadius = .1f;

	private Transform m_CeilingCheck;
	const float k_CeilingRadius = .01f;

	private Animator m_Anim;
	private Rigidbody2D m_Rigidbody2D;

	private bool m_FacingRight = true;

	private float m_Input;
	private bool m_Grounded;
	private bool m_Crouched;
	private bool m_Jump;
	private bool m_JumpFinished;
	private bool m_Dash;

	private float m_JumpTimer = 0.0f;


	#endregion


	public Vector2 Forward
	{
		get { return m_FacingRight ? Vector2.right : Vector2.left; }
	}


	#region Monobehaviours


	private void Awake ()
	{
		m_GroundCheck = transform.Find ("GroundCheck");
		m_CeilingCheck = transform.Find ("CeilingCheck");
		m_Anim = GetComponent<Animator> ();
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
	}


	private void FixedUpdate ()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll (m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for ( int i = 0; i < colliders.Length; i++ )
		{
			if ( colliders[i].gameObject != gameObject )
			{
				m_Grounded = true;
				break;
			}

		}
		m_Anim.SetBool ("Ground", m_Grounded);

		m_Anim.SetFloat ("vSpeed", m_Rigidbody2D.velocity.y);

		HandleCrouch ();
		HandleJump ();
		HandleDash ();
		Move ();
	}


	#endregion

	private void HandleCrouch ()
	{
		if ( !m_Crouched && m_Anim.GetBool ("Crouch") )
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if ( Physics2D.OverlapCircle (m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround) )
			{
				m_Crouched = true;
			}
		}

		// Set whether or not the character is crouching in the animator
		m_Anim.SetBool ("Crouch", m_Crouched);
	}


	private void Move ()
	{
		m_Input = ( m_Crouched ? m_Input * m_CrouchSpeed : m_Input );

		m_Anim.SetFloat ("Speed", Mathf.Abs (m_Input));

		m_Rigidbody2D.velocity = new Vector2 (m_Input * m_MoveSpeed, m_Rigidbody2D.velocity.y);

		if ( m_Input > 0 && !m_FacingRight || m_Input < 0 && m_FacingRight )
		{
			Flip ();
		}
	}


	private void HandleDash ()
	{
		if ( !m_Dash || m_Crouched )
			return;

		m_Rigidbody2D.MovePosition (m_Rigidbody2D.position + Forward * m_DashDistance);
		Instantiate (poof, m_Rigidbody2D.position, Quaternion.identity);
		m_Dash = false;
	}


	private void HandleJump ()
	{
		if ( m_Jump )
		{
			if ( m_Grounded && m_Anim.GetBool ("Ground") )
			{
				m_Grounded = false;
				m_JumpFinished = false;
				m_JumpTimer = 0;
				m_Anim.SetBool ("Ground", false);
				m_Rigidbody2D.AddForce (Vector2.up * m_JumpForce);
			}

			m_Jump = false;
		}
		else if ( !m_Grounded )
		{
			float force = m_JumpFinished ? m_JumpFinishedCounterForce : m_JumpCounterForce;
			m_Rigidbody2D.AddForce (Vector3.down * force);
		}
	}


	private void Flip ()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	#region Exposed Methods

	public void ProvideInput (float input)
	{
		m_Input = input;
	}


	public void SetCrouch (bool crouched)
	{
		this.m_Crouched = crouched;
	}


	public void Dash ()
	{
		m_Dash = Inventory?.Contains (x => x.Name == "Boots of Passion") ?? false;
	}


	public void StartJump ()
	{
		m_Jump = true;
	}


	public void FinishJump ()
	{
		m_JumpFinished = true;
	}

	#endregion
}
