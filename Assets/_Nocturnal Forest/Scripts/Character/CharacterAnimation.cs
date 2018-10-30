using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : CharacterBase
{
	public const string GROUNDED = "Ground", SPEED = "Speed", CROUCHED = "Crouch", ATTACK = "Attack", ALIVE = "Alive";

	private Animator m_Animator;

	private void Awake()
	{
		m_Animator = GetComponent<Animator>();
	}

	public void Trigger(string key)
	{
        m_Animator.SetTrigger(key);
	}

	public void SetBool(string key, bool value)
	{
		m_Animator.SetBool(key, value);
	}

	public bool GetBool(string key)
	{
		return m_Animator.GetBool(key);
	}

	public void SetFloat(string key, float value)
	{
		m_Animator.SetFloat(key, value);
	}

	public float GetFloat(string key)
	{
		return m_Animator.GetFloat(key);
	}
}
