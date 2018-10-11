using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterBase
{
    #region Exposed Fields

    [SerializeField] private float m_MoveAccel = 10f;
    [SerializeField] private float m_MaxSpeed = 10f;
    [SerializeField] private float m_JumpForce = 550f;
    [SerializeField] private float m_JumpCounterForce = 10;
    [SerializeField] private float m_JumpFinishedCounterForce = 30f;
    [SerializeField] private float m_DashDistance = 5.0f;
    [SerializeField] private GameObject m_Poof;
    [SerializeField] [Range(0, 1)] private float m_AirMultiplier = 0.25f;
    [SerializeField] [Range(0, 1)] private float m_CrouchMultiplier = 0.25f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject xpBar;

    #endregion

    #region Hidden Fields

    private Transform m_GroundCheck;
    const float k_GroundedRadius = 0.05f;

    private Transform m_CeilingCheck;
    const float k_CeilingRadius = 0.01f;

    private Rigidbody2D m_Rigidbody2D;

    private bool m_FacingRight = true;

    private float m_DesiredInput;
    private float m_Input;
    private bool m_Grounded;
    private bool m_Crouched;
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


    #region Monobehaviours


    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
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
        Anim.SetBool(CharacterAnimation.GROUNDED, m_Grounded);

        Anim.SetFloat(CharacterAnimation.VSPEED, m_Rigidbody2D.velocity.y);

        float accel = m_MoveAccel;
        if (!m_Grounded)
        {
            accel *= m_AirMultiplier;
        }
        else if (m_Crouched)
        {
            accel *= m_CrouchMultiplier;
        }

        m_Input = Mathf.Lerp(m_Input, m_DesiredInput, Time.deltaTime * accel);

        HandleCrouch();
        HandleJump();
        HandleDash();
        Move();

        //HealthStuff
        float healthPercent = (float)Health.Value / (float)Health.Max;
        float xpPercent = 0f;

        Vector3 temp = healthBar.transform.localScale;
        temp.x = healthPercent;
        healthBar.transform.localScale = temp;

        temp = xpBar.transform.localScale;
        temp.x = xpPercent;
        xpBar.transform.localScale = temp;
    }


    #endregion


    private void Move()
    {
        Vector2 current = Vector2.Scale(m_Rigidbody2D.velocity, Vector2.right);
        Vector2 input = Vector2.right * m_Input * m_MaxSpeed;

        m_Rigidbody2D.AddForce(input - current, ForceMode2D.Impulse);

        if (m_Input > 0 && !m_FacingRight || m_Input < 0 && m_FacingRight)
        {
            Flip();
        }
        
        Anim.SetFloat(CharacterAnimation.SPEED, Mathf.Abs(m_Input));
    }


    private void HandleCrouch()
    {
        if (!m_Crouched && Anim.GetBool(CharacterAnimation.CROUCHED))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                m_Crouched = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        Anim.SetBool(CharacterAnimation.CROUCHED, m_Crouched);
    }


    private void HandleDash()
    {
        if (m_NextDashTime > Time.time)
        {
            return;
        }

        if (!m_Dash || m_Crouched)
        {
            return;
        }

        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + Forward * m_DashDistance);
        Instantiate(m_Poof, m_Rigidbody2D.position, Quaternion.identity);
        m_Dash = false;
    }


    private void HandleJump()
    {
        if (m_Jump)
        {
            if (m_Grounded)
            {
                m_Grounded = false;
                m_JumpFinished = false;
                Anim.SetBool(CharacterAnimation.GROUNDED, false);
                m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
            }

            m_Jump = false;
        }
        else if (!m_Grounded)
        {
            float force = m_JumpFinished ? m_JumpFinishedCounterForce : m_JumpCounterForce;
            m_Rigidbody2D.AddForce(Vector3.down * force);
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


    public void SetCrouch(bool crouched)
    {
        this.m_Crouched = crouched;
    }


    public void Dash()
    {
        m_Dash = Inventory?.Contains(x => x.Name == "Boots of Passion") ?? false;
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
