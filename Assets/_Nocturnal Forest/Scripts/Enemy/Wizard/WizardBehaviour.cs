using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WizardBehaviour : EnemyBase
{
	[Header("Shooting")]
	[SerializeField] private WizardProjectile m_ProjectilePrefab;
	[SerializeField] private float m_ShootSpeed = 2;

	[Header("Warp")]
	public float maxWarpDistance = 15.0f;
	public float warpCooldownTime = 10.0f;

	private float m_NextAllowedWarpTime;
	private float m_NextAllowedShootTime;
	private Rigidbody2D m_Rigidbody2D;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}


	private void Update()
	{
		Shoot();
		Warp();
	}

	private void FixedUpdate()
	{

	}

	public void Shoot()
	{
		if (Time.time < m_NextAllowedShootTime)
		{
			return;
		}

		if (Character.Current == null || Vector2.Distance(Character.Current.transform.position, transform.position) > 25f)
		{
			return;
		}

		WizardProjectile projectile = Instantiate(m_ProjectilePrefab, transform.position, Quaternion.identity);
		projectile.Direction = Character.Current.transform.position - transform.position;

		m_NextAllowedShootTime = Time.time + 1 / m_ShootSpeed;
	}

	public void Warp()
	{
		if (Time.time < m_NextAllowedWarpTime)
		{
			return;
		}

		m_NextAllowedWarpTime = Time.time + warpCooldownTime;

		List<PathNode> locations = FindObjectsOfType<PathNode>().Where(x => Vector2.SqrMagnitude(transform.position - x.transform.position) < maxWarpDistance * maxWarpDistance).ToList();

		PathNode node = locations.PickRandom();

		if (node == null) { return; }

		transform.position = node.transform.position;
		Agent.SetDestination(null);
		Agent.SetCurrentToClosest();
	}
}
