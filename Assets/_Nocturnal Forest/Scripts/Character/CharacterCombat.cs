using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : CharacterBase
{
	public int baseAttackDamage = 8;
	public float baseAttackSpeed = 2.0f;
	public float baseAttackDistance = 1.0f;

	private float m_NextAttackTime;

	public Vector2 AttackForward
	{
		get { return Vector2.right * Mathf.Sign(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x); }
	}

	public void Attack()
	{
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

	private int CalculateDamage()
	{
		return baseAttackDamage + Inventory.GetStatBoost("Damage");
	}
}
