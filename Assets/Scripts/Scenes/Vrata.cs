using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vrata : MonoBehaviour
{
    [SerializeField] GameObject Slovo;
    [SerializeField] Transform player; 
    [SerializeField] Vector3 teleportPosition; 

    [SerializeField] private AudioClip doorSound;

    private bool canTeleport = false; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Slovo.SetActive(true);
            canTeleport = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            Slovo.SetActive(false);
            canTeleport = false; 
        }
    }

    private void Update()
    {
       
        if (canTeleport && Input.GetKeyDown(KeyCode.R))
        {
            TeleportPlayer();
        }
    }

   private void TeleportPlayer()
{
    Vector3 adjustedPosition = new Vector3(teleportPosition.x, teleportPosition.y - 0.25f, teleportPosition.z);
    player.position = adjustedPosition;
    SoundManager.instance.PlaySound(doorSound);
}

}
