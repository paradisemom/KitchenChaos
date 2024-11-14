
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;
    private void Start() {
        cuttingCounter.OnProgressChanged+=CuttingCounter_OnProgressChanged;
        barImage.fillAmount=0f;
        Hide();
    }
    private void CuttingCounter_OnProgressChanged(object sender,CuttingCounter.OnProgressChangedEventArgs e){
        barImage.fillAmount=e.progressNromalized;
        if(e.progressNromalized==0f||e.progressNromalized==1f){
            Hide();
        }else{
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    } 
    private void Hide(){
        gameObject.SetActive(false);
    }
}
