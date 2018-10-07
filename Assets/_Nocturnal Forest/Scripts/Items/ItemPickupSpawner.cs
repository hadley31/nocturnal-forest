using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupSpawner : MonoBehaviour
{
	public ItemPickup pickupPrefab;
	public List<Item> items;
	public bool spawnOnStart = false;
	public bool addRandomForce = true;
	public float randomForceMagnitude = 5f;

	private void Start()
	{
		if (spawnOnStart)
		{
			DropAll();
		}
	}

	public void DropAll()
	{
		DropAll(transform.position);
	}

	public void DropAll(Vector3 position)
	{
		foreach (Item i in items)
		{
			Spawn(i, position);
		}
	}

	public void DropOne()
	{
		DropOne(transform.position);
	}

	public void DropOne(Vector3 position)
	{
		if (items != null && items.Count > 0)
		{
			Spawn(items[Random.Range(0, items.Count)], position);
		}
	}

	private void Spawn(Item item, Vector2 position)
	{
		if (item == null)
		{
			return;
		}

		ItemPickup pickup = Instantiate(pickupPrefab, position, Quaternion.identity);
		pickup.Prime(item);

		if (addRandomForce)
		{
			Vector2 force = (Vector2.up + Vector2.right * Random.Range(-1.0f, 1.0f)).normalized * randomForceMagnitude;
			pickup.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
		}
	}
}
