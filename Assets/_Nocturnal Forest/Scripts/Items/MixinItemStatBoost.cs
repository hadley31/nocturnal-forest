using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixinItemStatBoost : MonoBehaviour
{
	[SerializeField] private string m_Stat;
	[SerializeField] private int m_Amount;

	public string Stat
	{
		get { return m_Stat; }
	}

	public int Amount
	{
		get { return m_Amount; }
	}
}
