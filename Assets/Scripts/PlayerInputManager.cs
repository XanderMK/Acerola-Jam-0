using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Input Manager", menuName = "Input/Player Input Manager")]
public class PlayerInputManager : ScriptableObject, PlayerInput.IPlayerActions
{
    public event UnityAction<Vector2> moveEvent = delegate {};
    public event UnityAction<Vector2> lookEvent = delegate {};
    public event UnityAction startRunEvent = delegate {};
    public event UnityAction stopRunEvent = delegate{};

    public event UnityAction interactEvent = delegate {};

    private PlayerInput playerInput;

    private void OnEnable() {
        if (playerInput == null) {
            playerInput = new PlayerInput();
            playerInput.Player.SetCallbacks(this);
        }

        EnablePlayerInput();
    }

    private void OnDisable() {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }
        else if (context.canceled) {
            moveEvent.Invoke(Vector2.zero);
        }
    }

    public void OnLook(InputAction.CallbackContext context) {
        if (context.performed) {
            // Make delta independent of screen size
            Vector2 value = context.ReadValue<Vector2>();

            value /= Screen.height;

            // Value is really small, so multiply by large value
            value *= 1080f;

            lookEvent.Invoke(value);
        }
        else {
            lookEvent.Invoke(Vector2.zero);
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.performed) {
            startRunEvent.Invoke();
        } 
        else if (context.canceled) {
            stopRunEvent.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) {
            interactEvent.Invoke();
        }
    }

    public void EnablePlayerInput() {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }

    public void EnableUIInput() {
        playerInput.Player.Disable();
        playerInput.UI.Enable();
    }

    public void DisableAllInput() {
        playerInput.Player.Disable();
        playerInput.UI.Disable();
    }
}
