using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManabar : MonoBehaviour
{
    private Slider m_Slider;

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
    }


    private void OnEnable()
    {
        Character.Current?.onXpChanged?.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        Character.Current?.onXpChanged?.RemoveListener(OnChanged);
    }

    private void OnChanged(int value)
    {
        m_Slider.value = value;
    }
}
