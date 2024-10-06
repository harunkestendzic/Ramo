using UnityEngine;
using UnityEngine.EventSystems;

public class NastaviButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UIManager uiManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("DF");
        //uiManager.UnpauseGame();
        uiManager.PauseGame(false);
    }
}
