using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateCanvasButton : MonoBehaviour, IPointerClickHandler
{
    public Canvas targetCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("No Canvas assigned to ActivateCanvasButton script.");
        }
    }
}
