using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Awake() {
    }
    private void Update() {
        Vector2 inputVector=gameInput.GetInputVectorNormalized();
        Vector3 myDir=new Vector3(inputVector.x,0f,inputVector.y);
        transform.position+=myDir*moveSpeed*Time.deltaTime;
        isWalking=myDir!=Vector3.zero;
        float rotateSpeed=10f;
        transform.forward=Vector3.Slerp(transform.forward,-myDir,Time.deltaTime*rotateSpeed);
        
    }
    public bool IsWalking(){
        return isWalking;
    }
}
