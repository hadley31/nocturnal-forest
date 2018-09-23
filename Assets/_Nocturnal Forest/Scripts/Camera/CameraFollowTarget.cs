using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
	#region Public Fields

	public Transform target;
	public float horizontalSpeed = 1;
	public float verticalSpeed = 1;

	#endregion

	#region Hidden Fields

	private const float m_OffsetZ = -10;
	private Vector3 m_CurrentVelocity;

	#endregion

	#region Monobehaviours


	private void Start ()
	{
		transform.parent = null;
	}


	private void LateUpdate ()
	{
		Follow ();
	}


	#endregion


	private void Follow ()
	{
		if ( target == null )
			return;

		float x = Mathf.Lerp (transform.position.x, target.position.x, Time.deltaTime * horizontalSpeed);
		float y = Mathf.Lerp (transform.position.y, target.position.y, Time.deltaTime * verticalSpeed);

		transform.position = new Vector3 (x, y, m_OffsetZ);
	}
}
