using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
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
     private bool HasRecipeSO(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO=GetFryingRecipeSOForIntput(inputKitchenObjectSO);
        return fryingRecipeSO!=null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO=GetFryingRecipeSOForIntput(inputKitchenObjectSO);
        if(fryingRecipeSO!=null){
            return fryingRecipeSO.output;
        }else{
            return null;
        }
    }
    private FryingRecipeSO GetFryingRecipeSOForIntput(KitchenObjectSO InputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if(fryingRecipeSO.input==InputKitchenObjectSO){
                return fryingRecipeSO;
            }
        }
        return null;
    }

}
