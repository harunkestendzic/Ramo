using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateKey : MonoBehaviour
{
    public GameObject keyObject;

    public void ActivateTargetObject()
    {
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Target keyObject is not assigned.");
        }
    }
}
