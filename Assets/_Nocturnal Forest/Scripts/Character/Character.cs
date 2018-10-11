using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Character : CharacterBase
{
    public static Character Current
    {
        get;
        private set;
    }

    public UnityEvent onSpawn;
    public UnityEvent onDie;

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
            pickup.Pickup(this.Inventory);
        }
    }

    public void Spawn(){
        onSpawn.Invoke();
    }

    public void Die(){
        onDie.Invoke();
    }
}
