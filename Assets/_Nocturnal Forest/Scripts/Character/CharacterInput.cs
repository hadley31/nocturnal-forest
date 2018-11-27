using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : CharacterBase
{
    private void Update()
    {
        if (!Character.Alive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        }

        HandleMovementInput();
        HandleCombatInput();
        Health.DoCountDown();
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
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            Combat.ProcessAttack();
        }
    }


    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Movement.StartJump();
        }

        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
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
