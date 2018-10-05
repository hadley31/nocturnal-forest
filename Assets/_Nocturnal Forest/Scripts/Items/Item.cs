using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Fields exposed in inspector
    [SerializeField] private int m_ID;
    [SerializeField] private string m_Desc;
    [SerializeField] private string m_Type;
    [SerializeField] private bool m_Consumable;
    [SerializeField] private Sprite m_Sprite;
    [SerializeField] private int m_MaxInInventory;

    #endregion

    #region Properties

    public string Name
    {
        get { return name; }
    }
    public int ID
    {
        get { return m_ID; }
    }
    public string Desc
    {
        get { return m_Desc; }
    }
    public string Type
    {
        get { return m_Type; }
    }
    public bool Consumable
    {
        get { return m_Consumable; }
    }
    public Sprite Sprite
    {
        get { return m_Sprite; }
    }
    public int MaxInInventory
    {
        get { return m_MaxInInventory; }
    }

    #endregion

    public override bool Equals(object obj)
    {
        return obj is Item && ((Item)obj).ID == this.ID;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"Item [id:{ID},name:{Name},type:{Type}";
    }
}