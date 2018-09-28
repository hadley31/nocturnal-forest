using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
	public Text itemName;
	public Text itemDesc;
	public Text itemType;
	public Image itemSprite;

	public Item Item
	{
		get;
		private set;
	}

	public void Prime (Item item)
	{
		Item = item;

		if ( Item == null )
			return;

		if ( itemName )
			itemName.text = Item.Name;
		if ( itemDesc )
			itemDesc.text = Item.Desc;
		if ( itemType )
			itemType.text = Item.Type;
		if ( itemSprite )
			itemSprite.sprite = Item.Sprite;
	}
}
