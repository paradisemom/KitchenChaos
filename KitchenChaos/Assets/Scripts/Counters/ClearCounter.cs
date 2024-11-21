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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    //player is holding plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                    
                }else{
                    //if player if not holding plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else{
                ////if player hasn't kithenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
       }
    }

}
