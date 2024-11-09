using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKitchenObjectPraent : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
   
    public void Interact(Player player){
        if(kitchenObject==null){
            Transform KitchenObjectTransform=Instantiate(kitchenObjectSO.prefab,counterTopPoint);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }else{
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    public Transform GetKichenObjectFllowTransform(){
        return counterTopPoint;
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
