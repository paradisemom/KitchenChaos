using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenGameobjectSO;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
        //if there is no kitchenObject
        if(player.HasKitchenObject()){
            //if player has kithenObject
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }else{
            //if player hasn't kithenObject
        }
       }else{
        //if there has kitchenObject
            if(player.HasKitchenObject()){
                //if player has kithenObject
            }else{
                ////if player hasn't kithenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
       }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject()){
            //There is a kitchenObject
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenGameobjectSO,this);  
        }
    }
}
