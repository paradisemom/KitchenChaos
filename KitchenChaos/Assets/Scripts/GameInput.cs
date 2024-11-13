using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public EventHandler OnInteract;
    public EventHandler OnInteractAlternate;
    private PlayerInputActions inputActions;

    private void Awake() {
        inputActions=new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed +=Interact_Performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }
    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteract?.Invoke(this,EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternate?.Invoke(this,EventArgs.Empty);
    }
    public Vector2 GetInputVectorNormalized(){
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        inputVector=inputVector.normalized;
        return inputVector;
    }
}