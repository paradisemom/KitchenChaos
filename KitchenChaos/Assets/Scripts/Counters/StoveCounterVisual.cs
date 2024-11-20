using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particleGameObject;
    private void Start() {
        stoveCounter.OnStateChanged+=StoveCounter_OnstateChanged;
    }
    private void StoveCounter_OnstateChanged(object sender,StoveCounter.OnStateChangedEventArgs e){
        bool visualShow=e.state==StoveCounter.State.Frying||e.state==StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(visualShow);
        particleGameObject.SetActive(visualShow);
    }
}
