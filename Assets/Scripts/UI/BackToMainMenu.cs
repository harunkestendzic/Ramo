using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
