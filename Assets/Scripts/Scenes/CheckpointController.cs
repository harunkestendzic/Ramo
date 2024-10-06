using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Checkpoint positionCheck;
    private Vector3 originalStartPosition;

    public void Start()
    {
       
        originalStartPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (PlayerPrefs.GetInt("isRewarded") == 0)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = originalStartPosition;
        }
        else if (PlayerPrefs.GetInt("isRewarded") == 1)
        {
            positionCheck.posX = PlayerPrefs.GetFloat("x");
            positionCheck.posY = PlayerPrefs.GetFloat("y");
            positionCheck.posZ = PlayerPrefs.GetFloat("z");

            GameObject.FindGameObjectWithTag("Player").transform.position = 
            new Vector3(positionCheck.posX, positionCheck.posY, positionCheck.posZ);
            
            PlayerPrefs.SetInt("isRewarded", 0);
        }
    }
}
