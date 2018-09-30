using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectStuff : MonoBehaviour {

    public Material mat;

    public float timeToCompleteCycle = 300.0f; //How many real seconds does day or night last together.

    public float transitionTime = 5.0f; //The time to transition from day to evening to night.

    private bool isNight = false;

    private float currentBlend = 0.0f;
    private float timePassed = 0.0f;

    void OnRenderImage( RenderTexture src, RenderTexture dest)
    {
        if (timePassed > (timeToCompleteCycle / 2.0))
        {
            //transition phase
            float incAmount = Time.deltaTime / (transitionTime / 2.0f);
            float incAmount2 = Time.deltaTime / (transitionTime);

            if (currentBlend <= 2.0f && isNight==false)
            {
                currentBlend += incAmount;
            }
            else if (currentBlend<=3.0f && isNight==true)
            {
                currentBlend += incAmount2;
            }
            else
            {
                if (isNight == false)
                    currentBlend = 2.0f;
                else
                    currentBlend = 0.0f;

                isNight = !isNight;

                timePassed = 0.0f;
            }

        }
        else
        {
            timePassed += Time.deltaTime;
        }

        mat.SetFloat("blendFactor", currentBlend);
        Graphics.Blit(src, dest, mat);
    }
}
