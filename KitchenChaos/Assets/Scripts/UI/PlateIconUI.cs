using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start() {
        plateKitchenObject.OnIngredientAdded+=PlateKitchenObject_OningredientAdded;
    }

    private void PlateKitchenObject_OningredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UPdateVisual();
    }
    private void UPdateVisual(){
        foreach(Transform child in transform){
            if(child==iconTemplate)continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform=Instantiate(iconTemplate,transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
