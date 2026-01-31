using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour
{
    InputSystem_Actions inputActions;
    public Vector2 movement;
    public bool jumpPressed;
    public float JumpPressedTime;
    public bool dashPressed;
    public bool attackPressed;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    
    public void move(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            movement = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            movement = Vector2.zero;
        }
    }

    public void jump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpPressedTime = Time.time;
            jumpPressed = true;
        }
        else if (context.performed)
        {
            jumpPressed = false;
            
        }
        else if(context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dashPressed = true;
        }
        else if (context.canceled)
        {
            dashPressed = false;
        }
    }

    public void onAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackPressed = true;
        }
        else if (context.performed)
        {
            attackPressed = false;
        }
        else if(context.canceled)
        {
            attackPressed = false;
        }
    }
}
