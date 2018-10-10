using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : CharacterBase
{
    private void Update()
    {
        HandleMovementInput();
        HandleCombatInput();
    }


    private void HandleMovementInput()
    {
        HandleCrouchInput();
        HandleJumpInput();

        float input = Input.GetAxisRaw("Horizontal");
        Movement.ProvideInput(input);
    }


    private void HandleCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Combat.Attack();
        }
    }


    private void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Movement.SetCrouch(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Movement.SetCrouch(false);
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
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F))
        {
            Movement.Dash();
        }
    }
}
