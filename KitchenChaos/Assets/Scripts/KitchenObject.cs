using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
        if(this.kitchenObjectParent!=null){
            this.kitchenObjectParent.ClearKitchenOject();
        }
        this.kitchenObjectParent=kitchenObjectParent;
        if(this.kitchenObjectParent.HasKitchenObject()){
            Debug.LogError("Counter already has KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent=kitchenObjectParent.GetKichenObjectFllowTransform();
        transform.localPosition=Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent(){
        return kitchenObjectParent;
    }
}

