using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnStateChangedEventArgs: EventArgs{
        public State state;
    }
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state=State.Idle;
    }
    private void Update() {
        
        if(HasKitchenObject()){
            switch (state){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer+=Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                        progressNromalized=fryingTimer/fryingRecipeSO.fryingTimerMax
                    });
                    
                    if(fryingTimer>fryingRecipeSO.fryingTimerMax){
                        //fired
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output,this);                 
                        state=State.Fried; 
                        burningTimer=0f;
                        burningRecipeSO=GetBurningRecipeSOForIntput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state=state
                        });
                        
                    }
                    break;
                case State.Fried:
                    burningTimer+=Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                        progressNromalized=burningTimer/burningRecipeSO.burningTimerMax
                    });
                    
                    if(burningTimer>burningRecipeSO.burningTimerMax){
                        //fired
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output,this);
                        state=State.Burned; 
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state=state
                        });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                        progressNromalized=0f
                    });
                    }
                    break;
                case State.Burned:
                    break;

            }
        }
    }
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
        //if there is no kitchenObject
        if(player.HasKitchenObject()){
            //if player has kithenObject
            if(HasRecipeSO(player.GetKitchenObject().GetKitchenObjectSO())){
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO=GetFryingRecipeSOForIntput(GetKitchenObject().GetKitchenObjectSO());
                state=State.Frying;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state=state
                        });
                
                fryingTimer=0f;
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                        progressNromalized=fryingTimer/fryingRecipeSO.fryingTimerMax
                    });
            }
            
            
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
                        state=State.Idle;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                                    state=state
                        });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                                progressNromalized=0f
                        });
                    }
                    
                }
                
            }else{
                ////if player hasn't kithenObject
                GetKitchenObject().SetKitchenObjectParent(player);
                state=State.Idle;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                            state=state
                });
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{
                        progressNromalized=0f
                });
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
    private BurningRecipeSO GetBurningRecipeSOForIntput(KitchenObjectSO InputKitchenObjectSO){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if(burningRecipeSO.input==InputKitchenObjectSO){
                return burningRecipeSO;
            }
        }
        return null;
    }

}
