using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Entity))]
public class Health : EntityBase
{
	[SerializeField]
	private int m_MaxHealth;

	[SerializeField]
	private bool m_GodMode;

	public IntUnityEvent onHealthChanged;
	public UnityEvent onDie;

    protected bool invincible = false;
    public int maxCoolDown = 20;

    private int countDown = 20;
	protected int m_health;

	public virtual int Max
	{
		get { return m_MaxHealth; }
		set { m_MaxHealth = value; }
	}

	public virtual int Value
	{
		get
		{
			return m_health;
		}
		private set
		{
			m_health = Mathf.Clamp(value, 0, m_MaxHealth);
			onHealthChanged.Invoke(m_health);
			if (m_health <= 0)
			{
				Die();
			}
		}
	}

	public virtual float Percent
	{
		get { return ((float)m_health) / m_MaxHealth; }
	}

	public virtual bool GodMode
	{
		get { return m_GodMode; }
		private set { m_GodMode = value; }
	}

	protected virtual void Start()
	{
		SetValueToMax();
	}

	public virtual void SetValue(int value)
	{
		Value = value;
	}
	public virtual void Decrease(int amount)
	{
		SetValue(Value - amount);
	}

	public virtual void SetValueToMax()
	{
		SetValue(m_MaxHealth);
	}

	public virtual void SetMaxValue(int maxValue, bool setValueToMax = false)
	{
		Max = maxValue;

		if (setValueToMax)
		{
			Value = Max;
		}
	}

	protected virtual void Die()
	{
		onDie.Invoke();
	}

	public virtual void Destroy()
	{
		Destroy(gameObject);
	}

    public bool GetInvicible()
    {
        return invincible;
    }

    public void DoCountDown()
    {
        if (invincible)
        {
            countDown -= 1;

            if (countDown <= 0)
            {
                EndHurtAnimation();
            }
        }
    }

    public virtual void BeginHurtAnimation()
    {
        invincible = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        countDown = maxCoolDown;
    }

    public virtual void EndHurtAnimation()
    {
        invincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        countDown = 0;
    }
}