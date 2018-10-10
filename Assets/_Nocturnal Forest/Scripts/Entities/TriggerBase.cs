using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : EntityBase
{
    private Trigger m_Trigger;
    public Trigger Trigger
    {
        get
        {
            if (m_Trigger == null)
            {
                m_Trigger = GetComponent<Trigger>();
            }
            return m_Trigger;
        }
    }
}
