using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    [SerializeField] private AudioClip keySound;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(keySound);
            door.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
