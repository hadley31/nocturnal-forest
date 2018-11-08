using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : CharacterBase
{
    private void Update()
    {
        if (!Character.Alive)
        {
            return;
        }

        HandleMovementInput();
        HandleCombatInput();
    }


    private void HandleMovementInput()
    {
        HandleJumpInput();
        HandleDashInput();

        float input = Input.GetAxisRaw("Horizontal");

        Movement.ProvideInput(input);
    }


    private void HandleCombatInput()
    {
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        if (Input.GetKeyDown(KeyCode.J))
        {
            Combat.Attack();
        }
    }


    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Movement.StartJump();
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            Movement.FinishJump();
        }
    }


    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Movement.Dash();
        }
    }
}
