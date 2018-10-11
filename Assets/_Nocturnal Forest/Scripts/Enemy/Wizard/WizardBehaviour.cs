using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehaviour : EnemyBase
{
	[Header("Warp")]
	public float maxWarpDistance = 15.0f;
	public float warpCooldownTime = 10.0f;

	private float m_NextAllowedWarpTime;

	private void Update()
	{

	}

	public void Warp()
	{
		if (Time.time < m_NextAllowedWarpTime)
		{
			return;
		}


	}
}
