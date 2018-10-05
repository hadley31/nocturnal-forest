using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour {

    private int timeToWarp = 2000; // in milliseconds
    private int timePassed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timePassed += (int)(Time.deltaTime*1000);

        Debug.Log(timePassed);
        if (timePassed >= timeToWarp)
        {
            GameObject[] points = GameObject.FindGameObjectsWithTag("Warp_Point");
            int length = points.Length;

            int whatPoint = Random.Range(0, length);

            Vector3 newPos = points[whatPoint].transform.position;
            this.transform.position = newPos;
           // Vector3 translateVec = this.transform.TransformPoint(newPos);
           // this.transform.Translate(newPos);

            timePassed = 0;
        }
	}
}
