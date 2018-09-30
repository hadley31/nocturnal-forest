using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectStuff : MonoBehaviour {

    public Material mat;

    public float timeToCompleteCycle = 600.0f; //How many real seconds does it take to complete a day night cycle
    //Currently, the default is a 10 minute cycle. 5 mins for day, 5 mins for night.

    private bool direction = false; //When false, going from day to night. When true, going from night to day.
    private float currentBlend = 0.0f;

    void OnRenderImage( RenderTexture src, RenderTexture dest)
    {

        float incAmount = Time.deltaTime / (timeToCompleteCycle / 2);
        
        if(direction==false)
        {
            currentBlend += incAmount;

            if (currentBlend >= 1.0f)
                direction = true;
        }
        else
        {
            currentBlend -= incAmount;

            if (currentBlend <= 0.0f)
                direction = false;
        }

        mat.SetFloat("blendFactor", currentBlend);
        Graphics.Blit(src, dest, mat);
    }
}
