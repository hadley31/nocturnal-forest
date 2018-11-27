using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButtonToQuit : MonoBehaviour
{
	public KeyCode key = KeyCode.Escape;
	public float holdTime = 2.5f;
	public bool dontDestroyOnLoad = true;

	private CanvasGroup group;
	private float timer = 0;

	private void Awake(){
		if (dontDestroyOnLoad){
			DontDestroyOnLoad(gameObject);
		}
		this.group = GetComponent<CanvasGroup>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(key))
		{
			timer = 0;
			group.alpha = 0;
		}
		else if (Input.GetKey(key))
		{
			timer += Time.deltaTime;
			group.alpha = timer / holdTime;
		}
		else if (Input.GetKeyUp(key))
		{
			group.alpha = 0;
		}

		if (timer >= holdTime)
		{
			Application.Quit();
		}
	}

    public static void quitEarly()
    {
        Application.Quit();
    }
}
