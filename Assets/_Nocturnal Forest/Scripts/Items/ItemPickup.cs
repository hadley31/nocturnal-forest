using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public static ItemPickup Spawn (ItemPickup prefab, Item item, Vector2 position, Quaternion rotation)
	{
		ItemPickup pickup = Instantiate (prefab, position, rotation);

		pickup.Prime (item);

		return pickup;
	}

	private SpriteRenderer m_Renderer;

	[SerializeField] private float rotateSpeed = 30.0f;
	[SerializeField] private float bobSpeed = 5.0f;
	[SerializeField] private float bobAmplitude = 0.5f;


	public Item Item
	{
		get;
		private set;
	}

	public SpriteRenderer Renderer
	{
		get
		{
			if ( !m_Renderer )
			{
				m_Renderer = GetComponentInChildren<SpriteRenderer> ();
			}
			return m_Renderer;
		}
	}

	public void Prime (Item item)
	{
		Item = item;
		Renderer.sprite = Item?.Sprite;
		Invoke ("EnableTrigger", 0.5f);
	}

	private void Update ()
	{
		Renderer.transform.localPosition = Vector3.up * (Mathf.Sin (Time.time * bobSpeed) * bobAmplitude + bobAmplitude);
		Renderer.transform.Rotate (0, rotateSpeed * Time.deltaTime, 0, Space.Self);
	}

	public void Pickup (Inventory inv)
	{
		print ($"Picked up item: {Item.Name}");

		if (Item != null)
		{
			inv?.Add (Item);
		}

		Destroy (gameObject);
	}

	private void EnableTrigger ()
	{
		GetComponentInChildren<CircleCollider2D> ().enabled = true;
	}
}