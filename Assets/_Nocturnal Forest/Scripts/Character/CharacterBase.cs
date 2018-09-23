using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
	private CharacterInput m_PlayerInput;
	public CharacterInput PlayerInput
	{
		get
		{
			if ( m_PlayerInput == null )
			{
				m_PlayerInput = GetComponent<CharacterInput> ();
			}
			return m_PlayerInput;
		}
	}

	private CharacterMovement m_Movement;
	public CharacterMovement Movement
	{
		get
		{
			if (m_Movement == null )
			{
				m_Movement = GetComponent<CharacterMovement> ();
			}
			return m_Movement;
		}
	}

	private CharacterAnimation m_Animation;
	public CharacterAnimation Anim
	{
		get
		{
			if (m_Animation == null )
			{
				m_Animation = GetComponent<CharacterAnimation> ();
			}
			return m_Animation;
		}
	}
}
