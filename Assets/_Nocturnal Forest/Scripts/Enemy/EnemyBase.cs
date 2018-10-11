using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
	private Enemy m_Enemy;
	public Enemy Enemy
	{
		get
		{
			if (m_Enemy == null)
			{
				m_Enemy = GetComponent<Enemy>();
			}
			return m_Enemy;
		}
	}

	private PathAgent m_PathAgent;
	public PathAgent Agent
	{
		get
		{
			if (m_PathAgent == null)
			{
				m_PathAgent = GetComponent<PathAgent>();
			}
			return m_PathAgent;
		}
	}
}
