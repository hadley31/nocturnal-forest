using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : EnemyBase
{

    public float jumpForce = 230.0f;
    public float horizontalForce = 130.0f;
    public float jumpCooldown = 2.0f;
    public int attackDamage = 3;
    public float attackCooldown = 2.0f;

    private bool m_Jump = false;
    private Rigidbody2D m_Rigidbody2D;
    private float m_NextJumpTime = 0;
    private float m_NextAttackTime = 0;

    // Use this for initialization
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > m_NextJumpTime)
        {
            m_Jump = true;
        }
        Health.DoCountDown();
    }

    private void FixedUpdate()
    {
        if (m_Jump)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (Time.time > m_NextAttackTime)
        {
            Attack(col.collider.GetComponent<Health>());
        }
    }

    private void Attack(Health health)
    {
        if (health == null || !health.Entity.Is<Character>())
        {
            return;
        }

        health.Decrease(attackDamage);
        health.BeginHurtAnimation();
        m_NextAttackTime = Time.time + attackCooldown;
    }

    private void Jump()
    {
        m_Jump = false;

        if (Character.Current == null)
        {
            return;
        }

        float direction = Mathf.Sign(Character.Current.transform.position.x - this.transform.position.x);

        m_Rigidbody2D.AddForce(Vector2.up * jumpForce + Vector2.right * direction * horizontalForce);

        m_NextJumpTime = Time.time + jumpCooldown;
    }
}
