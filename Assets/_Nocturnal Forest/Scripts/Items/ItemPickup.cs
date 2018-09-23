using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class ItemPickup : MonoBehaviour
{
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
				m_Renderer = GetComponent<SpriteRenderer> ();
			}
			return m_Renderer;
		}
	}

	public void Prime (Item item)
	{
		Item = item;
		Renderer.sprite = Item?.Sprite;
	}

	private void Update ()
	{
		transform.position += Vector3.up * Mathf.Sin (Time.time * bobSpeed) * bobAmplitude;
		transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
	}
}
