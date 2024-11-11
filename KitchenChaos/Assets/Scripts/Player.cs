using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour,IKitchenObjectParent
{
    public static Player Instance {get;private set;}
    public EventHandler<OnSelectedCounterChangedEvectArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEvectArgs:EventArgs{
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed=7f;
    
    [SerializeField] private Transform KitchenObjectHoldPoint;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    private bool isWalking;
    private Vector3 lastDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if(Instance!=null){
            Debug.LogError("there is more than one player");  
        }
        Instance=this;
    }
    private void Start() {
        gameInput.OnInteract+=GameInput_OnInteract;
    }
    private void GameInput_OnInteract(object sender,System.EventArgs e){
        if(selectedCounter!=null){
            selectedCounter.Interact(this);
        }
    }
    private void Update() {
        HandleMovement();
        HandleInteraction();
    }   
    public bool IsWalking(){
        return isWalking;
    }
    private void HandleInteraction(){
        Vector2 inputVector=gameInput.GetInputVectorNormalized();
        Vector3 moveDir=new Vector3(inputVector.x,0f,inputVector.y);
        if(moveDir!=Vector3.zero){
            lastDir=moveDir;
        }

        float InteractionDistance=2f;
        if( Physics.Raycast(transform.position,lastDir,out RaycastHit raycastHit,InteractionDistance,layerMask)){
                if(raycastHit.transform.TryGetComponent(out BaseCounter BaseCounter)){
                    if(BaseCounter!=selectedCounter){
                        SetSelectedCounter(BaseCounter);
                    }
                }else{
                    SetSelectedCounter(null);
                }
       }else{
            SetSelectedCounter(null);
       }

       
    }
    private void HandleMovement(){
        Vector2 inputVector=gameInput.GetInputVectorNormalized();
        Vector3 moveDir=new Vector3(inputVector.x,0f,inputVector.y);
        
        isWalking=moveDir!=Vector3.zero;
        float playerRadius=.7f;
        float playerHeight=2f;
        float moveDistance=moveSpeed*Time.deltaTime;
        bool canMove=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDir,moveDistance);
        float rotateSpeed=10f;
        if(!canMove){
            Vector3 moveDirX=new Vector3(moveDir.x,0,0).normalized;
            bool canMoveX=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDirX,moveDistance);
            if(canMoveX){
                transform.position+=moveDirX*moveDistance; 
            }else {
                Vector3 moveDirZ=new Vector3(0,0,moveDir.z).normalized;
                bool canMoveZ=!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,moveDirZ,moveDistance);
                if(canMoveZ){
                    transform.position+=moveDirZ*moveDistance; 
                }else{

                }
            }
        }
        if(canMove){
            transform.position+=moveDir*moveDistance; 
        }
        
        transform.forward=Vector3.Slerp(transform.forward,-moveDir,Time.deltaTime*rotateSpeed);
        
        
    }
    private void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter=selectedCounter;
        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEvectArgs{
              selectedCounter=selectedCounter
            });
    }
    public Transform GetKichenObjectFllowTransform(){
        return KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject=kitchenObject;
    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public void ClearKitchenOject(){
        kitchenObject=null;
    }
    public bool HasKitchenObject(){
        return kitchenObject!=null;
    }
}
