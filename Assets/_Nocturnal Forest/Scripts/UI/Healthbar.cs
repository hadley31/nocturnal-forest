using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
	[SerializeField] private Health m_Health;

	public virtual Health Health
	{
		get { return m_Health; }
	}

	
}
