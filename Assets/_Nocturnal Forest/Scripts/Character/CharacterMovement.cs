using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterBase
{
    #region Exposed Fields

    [SerializeField] private float m_MoveAccel = 10.0f;
    [SerializeField] private float m_MoveDeaccel = 15.0f;
    [SerializeField] private float m_MaxSpeed = 10.0f;
    [SerializeField] private float m_JumpForce = 550.0f;
    [SerializeField] private float m_JumpCounterForce = 10.0f;
    [SerializeField] private float m_JumpFinishedCounterForce = 30.0f;
    [SerializeField] private float m_DashDistance = 3.0f;
    [SerializeField] private float m_DashCooldown = 5.0f;
    [SerializeField] private bool m_MoveOnLadder = false;
    [SerializeField] private GameObject m_Poof;
    [SerializeField] private AudioClip whoosh;
    [SerializeField] [Range(0, 1)] private float m_AirMultiplier = 0.25f;
    [SerializeField] private LayerMask m_WhatIsGround;

    #endregion

    #region Hidden Fields

    private Transform m_GroundCheck;
    const float k_GroundedRadius = 0.05f;

    private Transform m_CeilingCheck;
    const float k_CeilingRadius = 0.01f;

    private Rigidbody2D m_Rigidbody2D;
    private CapsuleCollider2D m_Collider2D;

    private bool m_FacingRight = true;

    private float m_DesiredInput;
    private float m_Input;
    private bool m_Grounded;
    private bool m_Jump;
    private bool m_JumpFinished;
    private bool m_Dash;
    private float m_NextDashTime;

    #endregion


    public Vector2 Forward
    {
        get { return m_FacingRight ? Vector2.right : Vector2.left; }
    }

    public Vector2 Position
    {
        get { return transform.position; }
    }

    public bool OnLadder
    {
        get { return m_Collider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")); }
    }


    #region Monobehaviours


    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Collider2D = GetComponent<CapsuleCollider2D>();
    }


    private void FixedUpdate()
    {
        if (!Character.Alive)
        {
        //    return;
        }

        CheckGrounded();

        HandleInput();

        // Any vertical movement goes here
        HandleJump();
        HandleLadder();

        // No vertical movement should be below here
        HandleDash();
        Move();
    }


    #endregion


    private void CheckGrounded()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                break;
            }

        }
        //Anim.SetBool(CharacterAnimation.GROUNDED, m_Grounded);
    }

    private void HandleInput()
    {
        float accel = m_DesiredInput == 0 ? m_MoveDeaccel : m_MoveAccel;
        if (!m_Grounded)
        {
            accel *= m_AirMultiplier;
        }

        m_Input = Mathf.Lerp(m_Input, m_DesiredInput, Time.deltaTime * accel);
    }

    private void Move()
    {
        if (!OnLadder || m_Grounded || m_MoveOnLadder)
        {
            Vector2 current = Vector2.Scale(m_Rigidbody2D.velocity, Vector2.right);
            Vector2 input = Vector2.right * m_Input * m_MaxSpeed;
            m_Rigidbody2D.AddForce(input - current, ForceMode2D.Impulse);
        }
        else
        {
            m_Rigidbody2D.velocity = Vector2.Scale(m_Rigidbody2D.velocity, Vector2.up);
        }

        if (m_Input > 0 && !m_FacingRight || m_Input < 0 && m_FacingRight)
        {
            Flip();
        }

        Anim.SetFloat(CharacterAnimation.SPEED, Mathf.Abs(m_Input));
    }


    private void HandleDash()
    {
        if (Time.time < m_NextDashTime)
        {
            m_Dash = false;
            return;
        }

        if (!m_Dash)
        {
            return;
        }

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + Forward * m_DashDistance);
        Instantiate(m_Poof, m_Rigidbody2D.position, Quaternion.identity);
        Anim.Trigger(CharacterAnimation.DASH);
        Universe.PlaySound(whoosh, 0.1f);

        m_NextDashTime = Time.time + m_DashCooldown;
        m_Dash = false;
    }


    private void HandleLadder()
    {
        if (!OnLadder)
        {
            return;
        }

        m_Rigidbody2D.velocity = Vector2.Scale(m_Rigidbody2D.velocity, Vector2.right); // Set the y velocity to zero

        m_Rigidbody2D.AddForce(Vector2.up * Input.GetAxisRaw("Vertical") * 10000 * Time.fixedDeltaTime);
    }


    private void HandleJump()
    {
        if (m_Jump)
        {
            if (m_Grounded)
            {
                m_Grounded = false;
                m_JumpFinished = false;
                //Anim.SetBool(CharacterAnimation.GROUNDED, false);
                m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
            }

            m_Jump = false;
        }
        if (!m_Grounded)
        {
            if (!OnLadder)
            {
                float force = m_JumpFinished ? m_JumpFinishedCounterForce : m_JumpCounterForce;
                m_Rigidbody2D.AddForce(Vector3.down * force * Time.fixedDeltaTime);
            }

        }
        else if (m_Grounded)
        {
            // Here we are on the ground and not jumping so we add "stick to ground" force
            m_Rigidbody2D.AddForce(Vector2.down * 100);
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    #region Exposed Methods

    public void ProvideInput(float input)
    {
        m_DesiredInput = input;
    }


    public void Dash()
    {
        if(Inventory.Contains(x => x.Name == "Boots of Passion"))
        {
            m_Dash = true;
        }
    }


    public void StartJump()
    {
        m_Jump = true;
    }


    public void FinishJump()
    {
        m_JumpFinished = true;
    }

    #endregion
}
