using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKitchenObjectPraent : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


   
    public override void Interact(Player player){
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

}
