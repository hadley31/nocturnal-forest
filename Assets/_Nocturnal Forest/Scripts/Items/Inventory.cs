using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public UnityEvent onChanged;

    public void Add(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (items == null)
        {
            items = new List<Item>();
        }

        items.Add(item);
        onChanged.Invoke();
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
}
