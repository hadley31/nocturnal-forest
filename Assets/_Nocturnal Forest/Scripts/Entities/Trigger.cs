using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EntUnityEvent : UnityEvent<Entity> { }

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> { }

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public sealed class Trigger : EntityBase
{
	public bool fireEnterOnce = false;
	public EntUnityEvent onTriggerEnter;
	public EntUnityEvent onTriggerStay;
	public bool fireExitOnce = false;
	public EntUnityEvent onTriggerExit;

	private bool m_EnterFired = false;
	private bool m_ExitFired = false;

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

	private void Awake()
	{
		Collider = GetComponent<Collider2D>();
		Rigidbody = GetComponent<Rigidbody2D>();

		Collider.isTrigger = true;
		Rigidbody.isKinematic = true;
		Rigidbody.gravityScale = 0.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (m_EnterFired)
		{
			return;
		}

		Entity ent = other.GetComponent<Entity>();
		if (ent != null)
		{
			onTriggerEnter.Invoke(ent);
			m_EnterFired = true;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		Entity ent = other.GetComponent<Entity>();
		if (ent != null)
		{
			onTriggerStay.Invoke(ent);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (m_ExitFired)
		{
			return;
		}

		Entity ent = other.GetComponent<Entity>();
		if (ent != null)
		{
			onTriggerExit.Invoke(ent);
			m_ExitFired = true;
		}
	}

	#endregion
}