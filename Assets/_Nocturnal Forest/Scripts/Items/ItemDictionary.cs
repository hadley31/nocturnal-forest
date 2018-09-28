using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : ScriptableObject
{
	[SerializeField] private List<Item> items;

	public List<Item> GetAll (System.Predicate<Item> predicate)
	{
		return items.FindAll (predicate);
	}


	public Item Get (System.Predicate<Item> predicate)
	{
		return items.Find (predicate);
	}


	public Item Get (int id)
	{
		return Get (x => x.ID == id);
	}


	public Item Get (string name)
	{
		return Get (x => x.Name == name);
	}
}
