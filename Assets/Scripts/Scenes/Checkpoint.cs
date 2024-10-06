using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpoint;

    public float posX;
    public float posY;
    public float posZ;

    public void Start(){
        posX = checkpoint.position.x;
        posY = checkpoint.position.y;
        posZ = checkpoint.position.z;
    }

  

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            PlayerPrefs.SetFloat("x",posX);
            PlayerPrefs.SetFloat("y",posY);
            PlayerPrefs.SetFloat("z",posZ);
            

        }
    }

}
