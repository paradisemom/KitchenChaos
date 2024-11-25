
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgerssGameObject;
    private IHasProgress hasProgress;
    private void Start() {
        hasProgress=hasProgerssGameObject.GetComponent<IHasProgress>();
        if(hasProgress==null){
            Debug.LogError("Game Object"+hasProgerssGameObject+"dose not have a Component that implements IHasProgress");
        }
        hasProgress.OnProgressChanged+=HasProgerss_OnProgressChanged;
        barImage.fillAmount=0f;
        Hide();
    }
    private void HasProgerss_OnProgressChanged(object sender,IHasProgress.OnProgressChangedEventArgs e){
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
