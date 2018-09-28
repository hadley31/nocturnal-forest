using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EntUnityEvent : UnityEvent<Entity> { }

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> { }

[RequireComponent (typeof (Rigidbody2D)), RequireComponent (typeof (Collider2D))]
public sealed class Trigger : EntityBase
{
	public EntUnityEvent onTriggerEnter;
	public EntUnityEvent onTriggerStay;
	public EntUnityEvent onTriggerExit;

	public Collider2D Collider
	{
		get;
		private set;
	}
	public Rigidbody2D Rigidbody
	{
		get;
		private set;
	}

	#region Monobehaviours

	private void Awake ()
	{
		Collider = GetComponent<Collider2D> ();
		Rigidbody = GetComponent<Rigidbody2D> ();

		Collider.isTrigger = true;
		Rigidbody.isKinematic = true;
		Rigidbody.gravityScale = 0.0f;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		Entity ent = other.GetComponent<Entity> ();
		if ( ent != null )
		{
			onTriggerEnter.Invoke (ent);
		}
	}

	private void OnTriggerStay2D (Collider2D other)
	{
		Entity ent = other.GetComponent<Entity> ();
		if ( ent != null )
		{
			onTriggerStay.Invoke (ent);
		}
	}

	private void OnTriggerExit2D (Collider2D other)
	{
		Entity ent = other.GetComponent<Entity> ();
		if ( ent != null )
		{
			onTriggerExit.Invoke (ent);
		}
	}

	#endregion
}