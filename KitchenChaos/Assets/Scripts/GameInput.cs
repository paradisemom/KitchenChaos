using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private void Awake() {
        inputActions=new PlayerInputActions();
        inputActions.Player.Enable();
    }
    public Vector2 GetInputVectorNormalized(){
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        inputVector=inputVector.normalized;
        return inputVector;
    }
}
