using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CharacterHealthbar : MonoBehaviour
{
	private Slider m_Slider;

	private Health CharacterHealth
	{
		get { return Character.Current?.Health; }
	}

	private void Awake()
	{
		m_Slider = GetComponent<Slider>();
	}


	private void OnEnable()
	{
		if (CharacterHealth)
		{
			CharacterHealth.onHealthChanged.AddListener(OnChanged);
			m_Slider.maxValue = CharacterHealth.Max;
			m_Slider.value = CharacterHealth.Value;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		CharacterHealth?.onHealthChanged?.RemoveListener(OnChanged);
	}

	private void OnChanged(int value)
	{
		m_Slider.value = value;
	}
}
