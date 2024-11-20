using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cutProgress;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
        //if there is no kitchenObject
        if(player.HasKitchenObject()){
            //if player has kithenObject
            if(HasRecipeSO(player.GetKitchenObject().GetKitchenObjectSO())){
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cutProgress=0;
                CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOForIntput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                    progressNromalized=(float)cutProgress/cuttingRecipeSO.cutProgressMax
                });
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
            cutProgress++;
            OnCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOForIntput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                    progressNromalized=(float)cutProgress/cuttingRecipeSO.cutProgressMax
                });
            if(cutProgress>cuttingRecipeSO.cutProgressMax-1){
                
                KitchenObjectSO outputKitchenObjectSO=GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                //There is a kitchenObject
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO,this);  
            }
            
        }
    }
    private bool HasRecipeSO(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOForIntput(inputKitchenObjectSO);
        return cuttingRecipeSO!=null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        CuttingRecipeSO cuttingRecipeSO=GetCuttingRecipeSOForIntput(inputKitchenObjectSO);
        if(cuttingRecipeSO!=null){
            return cuttingRecipeSO.output;
        }else{
            return null;
        }
    }
    private CuttingRecipeSO GetCuttingRecipeSOForIntput(KitchenObjectSO InputKitchenObjectSO){
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input==InputKitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
