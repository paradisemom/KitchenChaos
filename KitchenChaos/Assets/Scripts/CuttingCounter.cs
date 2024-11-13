using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
        //if there is no kitchenObject
        if(player.HasKitchenObject()){
            //if player has kithenObject
            if(HasRecipeSO(player.GetKitchenObject().GetKitchenObjectSO())){
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            
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
        if(HasKitchenObject()&&HasRecipeSO(GetKitchenObject().GetKitchenObjectSO())){
            KitchenObjectSO outputKitchenObjectSO=GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            //There is a kitchenObject
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO,this);  
        }
    }
    private bool HasRecipeSO(KitchenObjectSO InputKitchenObjectSO){
         foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input==InputKitchenObjectSO){
                return true;
            }
        }
        return false;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO InputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input==InputKitchenObjectSO){
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
