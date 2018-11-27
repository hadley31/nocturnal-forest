﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void LoadCurrentScene(){
        Debug.Log("PRESSED BUTTON");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
	}
}
