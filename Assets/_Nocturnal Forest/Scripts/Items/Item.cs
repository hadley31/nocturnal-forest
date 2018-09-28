using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public string Name;
	public int ID;
	public string Desc;
	public string Type;
	public bool consumable;
	public Sprite Sprite;

	public Item (int id, string name, string desc, string type)
	{
		this.ID = id;
		this.Name = name;
		this.Desc = desc;
		this.Type = type;
	}

	public override bool Equals (object obj)
	{
		return obj is Item && ( (Item) obj ).ID == this.ID;
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public override string ToString ()
	{
		return $"Item [id:{ID},name:{Name},type:{Type}";
	}
}