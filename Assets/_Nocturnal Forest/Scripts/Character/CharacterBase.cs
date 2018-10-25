using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : EntityBase
{
    private Character m_Character;
    public Character Character
    {
        get
        {
            if (m_Character == null)
            {
                m_Character = GetComponent<Character>();
            }
            return m_Character;
        }
    }

    private Inventory m_Inventory;
    public Inventory Inventory
    {
        get
        {
            if (m_Inventory == null)
            {
                m_Inventory = GetComponent<Inventory>();
            }
            return m_Inventory;
        }
    }

    private CharacterInput m_PlayerInput;
    public CharacterInput PlayerInput
    {
        get
        {
            if (m_PlayerInput == null)
            {
                m_PlayerInput = GetComponent<CharacterInput>();
            }
            return m_PlayerInput;
        }
    }

    private CharacterMovement m_Movement;
    public CharacterMovement Movement
    {
        get
        {
            if (m_Movement == null)
            {
                m_Movement = GetComponent<CharacterMovement>();
            }
            return m_Movement;
        }
    }

    private CharacterCombat m_Combat;
    public CharacterCombat Combat
    {
        get
        {
            if (m_Combat == null)
            {
                m_Combat = GetComponent<CharacterCombat>();
            }
            return m_Combat;
        }
    }

    private CharacterAnimation m_Animation;
    public CharacterAnimation Anim
    {
        get
        {
            if (m_Animation == null)
            {
                m_Animation = GetComponent<CharacterAnimation>();
            }
            return m_Animation;
        }
    }
}
