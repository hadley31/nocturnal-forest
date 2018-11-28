using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempObjectScript : MonoBehaviour {

    private int time = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (time >= 1)
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }

        time++;
    }
}
