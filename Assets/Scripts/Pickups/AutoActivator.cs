using UnityEngine;

public class AutoActivator : MonoBehaviour
{
    private GameObject parentObject;
    private bool isTimerActive = false;
    private float timer = 0f;

    void Start()
    {
        parentObject = transform.parent.gameObject;
    }

    void Update()
    {
        if (parentObject != null && !parentObject.activeSelf)
        {
            if (!isTimerActive)
            {
                isTimerActive = true;
                timer = 3f; 
            }

            if (isTimerActive)
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    parentObject.SetActive(true);
                    isTimerActive = false; 
                }
            }
        }
    }
}
