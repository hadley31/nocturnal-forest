using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class Universe : MonoBehaviour
{
    public static Universe Instance
    {
        get;
        private set;
    }

    #region Monobehaviours

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("A Universe object already exists! Destroying new Universe!");
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        ControlUpdate();
    }

    #endregion

    #region Control Elements

    public static readonly List<ControlElement> controls = new List<ControlElement>();

    public static ControlElement ElementInControl
    {
        get;
        private set;
    }

    private void ControlUpdate()
    {
        ControlElement last = GetLastControl();

        if (ElementInControl == last && ElementInControl != null)
        {
            ElementInControl.OnControlUpdate();
        }
        else
        {
            ElementInControl?.OnLoseControl();
            last?.OnGainControl();

            ElementInControl = last;
        }
    }

    private ControlElement GetLastControl()
    {
        return controls.Count > 0 ? controls[controls.Count - 1] : null;
    }

    #endregion

    #region Sounds

    private AudioSource m_AudioSource;
    public AudioSource AudioSource
    {
        get
        {
            if (m_AudioSource == null)
            {
                m_AudioSource = GetComponent<AudioSource>();
            }

            return m_AudioSource;
        }
    }

    public static void PlaySound(AudioClip clip)
    {
        if (Instance?.AudioSource != null)
        {
            Instance.AudioSource.PlayOneShot(clip);
        }
		else
		{
			Debug.LogWarning("There is no audiosource on the Universe object!");
		}
    }

    #endregion
}
