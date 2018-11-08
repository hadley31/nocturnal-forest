using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat2 : CharacterBase {

    float timeBetweenInputs = 0.0f;
    float BufferTime1 = 0.5f; //Time allowed between 1st attack and 2nd attack to input
    float BufferTime2 = 0.5f; //Time allowed between 2nd attack and 3rd attack to input

    float previousTime = 0;

    int attackDamage = 4;
    int phase = 0;
    
    
    void ProcessAttacks()
    {
        if(phase==0)
        {
            //Not attacking and is free to start
            phase = 1;
            previousTime = Time.fixedTime;
            Anim.Trigger(CharacterAnimation.ATTACK);
        }
        else if(phase==1)
        {
            //Is attacking and is first attack
            timeBetweenInputs = Time.fixedTime - previousTime;
            if(timeBetweenInputs <= BufferTime1)
            {
                //can do second attack
                //Anim.Trigger(CharacterAnimation.ATTACK2);
            }
            else
            {
                //can't do second attack
                Anim.Trigger(CharacterAnimation.ATTACK1_RECOVERY);
            }
        }
        else if(phase==2)
        {
            timeBetweenInputs = Time.fixedTime - previousTime;
            if (timeBetweenInputs <= BufferTime2)
            {
                //can do third attack
                //Anim.Trigger(CharacterAnimation.ATTACK3);
            }
            else
            {
                //can't do third attack
                Anim.Trigger(CharacterAnimation.ATTACK2_RECOVERY);
            }
        }
        else if(phase==3)
        {
            //Last attack
        }
    }
}
