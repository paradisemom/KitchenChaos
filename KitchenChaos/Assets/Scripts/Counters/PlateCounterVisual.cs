using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;
    private void Awake() {
        plateVisualGameObjectList=new List<GameObject>();
    }
    private void Start() {
        plateCounter.OnPlateSpawned+=PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved+=PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject=plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender,System.EventArgs e){
        Transform plateTransform=Instantiate(plateVisualPrefab,counterTopPoint);
        float plateOffsetY=.1f;
        plateTransform.localPosition=new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateTransform.gameObject);
    }
}
