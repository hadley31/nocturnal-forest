using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimation : CharacterBase
{
	private Animator m_Animator;

	private void Awake ()
	{
		m_Animator = GetComponent<Animator> ();
	}


}
