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

    private int m_XP;

    public IntUnityEvent onXpChanged;

    public UnityEvent onSpawn;
    public UnityEvent onDie;


    public bool Alive
    {
        get;
        private set;
    }

    public int XP
    {
        get { return m_XP; }
        set
        {
            if (m_XP != value)
            {
                m_XP = value;
            }
        }
    }

    private void OnEnable()
    {
        if (Current == null)
        {
            Current = this;
            Spawn();
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

    public void Spawn()
    {
        Alive = true;
        Anim.SetBool(CharacterAnimation.ALIVE, true);
        onSpawn.Invoke();
    }

    public void Die()
    {
        Alive = false;
        Anim.SetBool(CharacterAnimation.ALIVE, false);
        onDie.Invoke();
    }
}
