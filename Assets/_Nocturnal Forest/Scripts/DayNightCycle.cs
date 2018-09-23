using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
	private float m_DayTimer = 0;

	[SerializeField] private float dayLength = 300f;

	private void Update ()
	{
		m_DayTimer -= Time.deltaTime;

		
	}
}
