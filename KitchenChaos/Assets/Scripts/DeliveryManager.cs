using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimeMax=4f;
    private int waitingRecipeMax=4;
    private void Awake() {
        Instance=this;
        waitingRecipeSOList=new List<RecipeSO>();
    }
    private void Update() {
        spawnRecipeTimer-=Time.deltaTime;
        if(spawnRecipeTimer<=0f){
            spawnRecipeTimer=spawnRecipeTimeMax;
            if(waitingRecipeSOList.Count<waitingRecipeMax){
                RecipeSO waitingRecipeSO= recipeListSO.recipeSOList[Random.Range(0,recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
            
        }
    }
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject){
        for(int i=0;i<waitingRecipeSOList.Count;i++){
            RecipeSO waitingRecipeSO=waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count==plateKitchenObject.GetKitchenObjectSOList().Count){
                // Have the same number ingerdient
                bool plateComtentMatchedRecipe=true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList){
                    //Cycleing all kitchen in recipelist
                    bool ingerdientFound=false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        //Cycleing all kitchen in plate
                        if(plateKitchenObjectSO==recipeKitchenObjectSO){
                            //ingredient mathed
                            ingerdientFound=true;
                            break;
                        }
                    }
                    if(!ingerdientFound){
                        plateComtentMatchedRecipe=false;
                    }
                }
                if(plateComtentMatchedRecipe){
                    //player has correct plate
                    Debug.Log("player has correct plate");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
                
            }
            
        }
        //no match
        //player did not delivery correct plate
        Debug.Log("player did not delivery correct plate");
    }
}
