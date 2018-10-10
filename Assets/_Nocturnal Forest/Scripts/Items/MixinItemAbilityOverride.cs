using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixinItemAbilityOverride : MonoBehaviour
{
    [SerializeField] private string m_Ability;
    [SerializeField] private bool m_Override = true;
    [SerializeField] private int m_Precedence = 0;

    public string Ability
    {
        get { return m_Ability; }
    }

    public bool Override
    {
        get { return m_Override; }
    }

    public int Precedence
    {
        get { return m_Precedence; }
    }
}
