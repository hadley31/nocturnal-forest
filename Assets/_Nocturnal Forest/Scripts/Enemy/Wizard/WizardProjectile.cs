using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class WizardProjectile : MonoBehaviour
{
	public float speed;
	public int damage;

	public Vector2 Direction
	{
		get;
		set;
	}

	private void Update()
	{
		transform.Translate(Direction.normalized * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			Destroy(gameObject);
			return;
		}

		Character c = other.GetComponent<Character>();

		if (c == null)
		{
			return;
		}

		c.Health.Decrease(damage);
		Destroy(gameObject);
	}
}
