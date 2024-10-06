using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NaScenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string SceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneName);
    }
}
