using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrintUtility : MonoBehaviour
{
	public void Log (string message)
	{
		Debug.Log (message);
	}

	public void Warn (string message)
	{
		Debug.LogWarning (message);
	}

	public void Error (string message)
	{
		Debug.LogError (message);
	}
}
