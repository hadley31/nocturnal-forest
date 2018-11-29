using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : CharacterBase
{
    public AudioSource swordSwing;

    public int baseAttackDamage = 8;
    public float baseAttackSpeed = 2.0f;
    public float baseAttackDistance = 1.0f;

    private float m_NextAttackTime;

    private bool canDoAttack2 = true;
    private bool canDoAttack3 = true;

    /*
     * Not used but provide good information
    private float endOfAttack1 = 7f/19f;
    private float endOfAttack2 = 11f/19f;

    private float attackFrame1 = 5f / 19f;
    private float attackFrame2 = 9f / 19f;
    private float attackFrame3 = 16f / 19f;
    */

    private bool initiatedAttack2 = false;
    private bool initiatedAttack3 = false;
    private bool isAttacking = false;

    protected virtual void Start()
    {
        swordSwing = GetComponent<AudioSource>();
    }
    public Vector2 AttackForward
    {
        get { return Vector2.right * Mathf.Sign(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x); }
    }

    public void ProcessAttack()
    {
        
        if (isAttacking == false)
        {
            //startAttack
            isAttacking = true;
            Anim.Trigger(CharacterAnimation.ATTACK);
            //swordSwing.Play();
            canDoAttack2 = true;
            canDoAttack3 = true;
        }
        else if ( canDoAttack2 == true)
        {
            //Do attack2
            initiatedAttack2 = true;
            //swordSwing.Play();
        }
        else if (canDoAttack3 == true)
        {
            //Do attack3
            initiatedAttack3 = true;
            //swordSwing.Play();
        }
    }

    private void oldAttackCode()
    {
        if (!Character.Alive)
        {
            return;
        }

        if (Time.time < m_NextAttackTime)
        {
            return;
        }

        // Animate
        Anim.Trigger(CharacterAnimation.ATTACK);

        // Increase the attack timer
        m_NextAttackTime = Time.time + 1 / baseAttackSpeed;

        // Calculate layer mask for everything but character. This is done by getting the layer mask for character and then finding the complement
        int mask = LayerMask.GetMask("Enemy");

        // Send out a raycast from our position
        RaycastHit2D hitInfo = Physics2D.Raycast(Movement.Position, Movement.Forward, baseAttackDistance, mask);

        if (!hitInfo)
        {
            // We didn't hit anything
            return;
        }

        // Look for a health component on the transform we hit
        Health health = hitInfo.transform.GetComponent<Health>();

        if (health == null)
        {
            // We hit something, but it cannot be damage
            return;
        }

        // If we have made it this far, we have hit something with a health component
        print($"We hit {health.name}");

        // Decrease the health of the entity we hit
        int damage = CalculateDamage();
        health.Decrease(damage);
    }

    public void GoToAttack2()
    {
        //Determines if we should continue on with our attack or go to
        //the first recovery animation

        if(initiatedAttack2==true)
        {
            //continue
            //Do nothing
            //swordSwing.Play();
            initiatedAttack2 = false;
            canDoAttack2 = false;
        }
        else
        {
            //stop animation
            //play first recovery animation
            Anim.Trigger(CharacterAnimation.ATTACK_REC_1);
            ResetAttack();
        }
    }

    public void GoToAttack3()
    {
        //Determines if we should continue on with our attack or go to
        //the second recovery animation

        if (initiatedAttack3 == true)
        {
            //continue
            //Do nothing
            //swordSwing.Play();
            initiatedAttack3 = false;
            canDoAttack3 = false;
        }
        else
        {
            //stop animation
            //play second recovery animation
            Anim.Trigger(CharacterAnimation.ATTACK_REC_2);
            ResetAttack();
        }
    }

    public void ResetAttack()
    {
        initiatedAttack2 = false;
        initiatedAttack3 = false;
        canDoAttack2 = false;
        canDoAttack3 = false;
        isAttacking = false;
    }

    public void Attack()
    {
        if (!Character.Alive)
        {
            return;
        }

        // Calculate layer mask for everything but character. This is done by getting the layer mask for character and then finding the complement
        int mask = LayerMask.GetMask("Enemy");

        // Send out a raycast from our position
        RaycastHit2D hitInfo = Physics2D.Raycast(Movement.Position, Movement.Forward, baseAttackDistance, mask);
        swordSwing.Play();

        if (!hitInfo)
        {
            // We didn't hit anything
            return;
        }

        // Look for a health component on the transform we hit
        Health health = hitInfo.transform.GetComponent<Health>();

        if (health == null)
        {
            // We hit something, but it cannot be damage
            return;
        }

        // If we have made it this far, we have hit something with a health component
        print($"We hit {health.name}");

        // Decrease the health of the entity we hit
        int damage = CalculateDamage();
        if (health.GetInvicible() == false)
        {
            health.Decrease(damage);
            health.BeginHurtAnimation();
        }
        else
        {
            //Can't hurt the entity yet
        }
    }

    private int CalculateDamage()
    {
        return baseAttackDamage + Inventory.GetStatBoost("Damage");
    }
}
