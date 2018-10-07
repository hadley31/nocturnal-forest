using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTimer : MonoBehaviour
{
	public float duration;
	public UnityEvent onFire;

	private float m_Timer;

	private void Update()
	{
		if (m_Timer <= 0)
		{
			onFire.Invoke();
			m_Timer = duration;
		}
		else
		{
			m_Timer -= Time.deltaTime;
		}
	}
}
