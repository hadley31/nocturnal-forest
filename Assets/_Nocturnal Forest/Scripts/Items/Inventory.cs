using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Inventory : MonoBehaviour
{
	public List<Item> items;
	public int maxItems = 20;
	public UnityEvent onChanged;

	public bool Add(Item item)
	{
		if (item == null)
		{
			return false;
		}

		if (items?.Count >= maxItems)
		{
			return false;
		}

		if (item.MaxInInventory > 0 && items?.Count(x => x == item) >= item.MaxInInventory)
		{
			return false;
		}

		if (items == null)
		{
			items = new List<Item>();
		}

		items.Add(item);
		onChanged.Invoke();

		return true;
	}

	public bool RemoveFirst(int id)
	{
		return items?.Remove(items.Find(x => x.ID == id)) ?? false;
	}

	public bool RemoveLast(int id)
	{
		return items?.Remove(items.FindLast(x => x.ID == id)) ?? false;
	}

	public bool RemoveAll(System.Predicate<Item> predicate)
	{
		return items.RemoveAll(predicate) > 0;
	}

	public bool Contains(System.Predicate<Item> predicate)
	{
		return items?.Exists(predicate) ?? false;
	}

	public int GetStatBoost(string stat)
	{
		return items.Select(x => x.GetStatBoosts()).Where(x => x.ContainsKey(stat)).Select(x => x[stat]).Sum();
	}

	public Dictionary<string, int> GetAllStatBoosts()
	{
		return items.Select(x => x.GetStatBoosts()).Aggregate((agg, x) =>
		{
			foreach (KeyValuePair<string, int> pair in x)
			{
				if (agg.ContainsKey(pair.Key))
				{
					agg[pair.Key] += pair.Value;
				}
				else
				{
					agg.Add(pair.Key, pair.Value);
				}
			}
			return agg;
		});
	}
}
