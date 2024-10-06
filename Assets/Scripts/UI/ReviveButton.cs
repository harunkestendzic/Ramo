using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReviveButton : MonoBehaviour, IPointerClickHandler
{
    public static ReviveButton instance;

    public void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) 
        {
        reviveButton();
        }
    }


    public void reviveButton()
    {
        PlayerPrefs.SetInt("isRewarded", 1);
        //SceneController.instance.ReloadLevelFromCheckpoint(); 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
