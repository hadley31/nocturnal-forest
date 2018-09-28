using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
	public List<Item> items;
	public UnityEvent onChanged;

	public void Add (Item item)
	{
		if (items == null )
		{
			items = new List<Item> ();
		}

		items.Add (item);
		onChanged.Invoke ();
	}

	public bool Remove (System.Predicate<Item> predicate)
	{
		return items?.Remove (items.Find (predicate)) ?? false;
	}

	public bool Contains (System.Predicate<Item> predicate)
	{
		return items?.Exists (predicate) ?? false;
	}
}
