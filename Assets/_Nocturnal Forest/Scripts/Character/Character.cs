using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Character : CharacterBase
{
    public static Character Current
    {
        get;
        private set;
    }

    private void OnEnable()
    {
        if (Current == null)
        {
            Current = this;
        }
    }

    private void OnDisable()
    {
        if (Current == this)
        {
            Current = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickup pickup = collision.GetComponent<ItemPickup>();
        if (pickup != null)
        {
            pickup.Pickup(Inventory);
        }
    }
}
