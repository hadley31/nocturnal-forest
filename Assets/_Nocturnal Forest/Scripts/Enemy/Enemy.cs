using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Entity))]
public class Enemy : EnemyBase
{
    public AudioSource slimeHurt;

    public readonly List<Enemy> All = new List<Enemy>();

	public UnityEvent onDie;

	private void OnEnable()
	{
		if (!All.Contains(this))
		{
			All.Add(this);
		}
	}

	private void OnDisable()
	{
		All.Remove(this);
	}

    protected virtual void Start()
    {
        slimeHurt = GetComponent<AudioSource>();
        //slimeDeath = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
	{
		Character c = collision.collider.GetComponentInParent<Character>();
      

		if (c != null)
		{
			Vector2 offset = collision.rigidbody.position - collision.otherRigidbody.position;
			if (Vector2.Dot(offset, Vector2.up) > 0.85f)
			{
				GetComponent<Health>().SetValue(0);
                
            }
		}
	}

	public void Die()
	{
		onDie.Invoke();
        slimeHurt.Play();
    }
}
