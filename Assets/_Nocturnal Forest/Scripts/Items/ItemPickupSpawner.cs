using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupSpawner : MonoBehaviour
{
	public ItemPickup pickupPrefab;
	public List<Item> items;
	public bool spawnOnStart = false;
	public bool addRandomForce = true;
	public float randomForceMagnitude = 25f;

	private void Start ()
	{
		if ( spawnOnStart )
		{
			Spawn ();
		}
	}

	public void Spawn ()
	{
		Spawn (transform.position);
	}

	public void Spawn (Vector3 position)
	{
		foreach ( Item item in items )
		{
			ItemPickup pickup = Instantiate (pickupPrefab, position, Quaternion.identity);
			pickup.Prime (item);

			if ( addRandomForce )
			{
				Vector2 force = (Vector2.up + Vector2.right * Random.Range (-1.0f, 1.0f)).normalized * randomForceMagnitude;
				pickup.GetComponent<Rigidbody2D> ().AddForce (force, ForceMode2D.Force);
			}
		}
	}
}
