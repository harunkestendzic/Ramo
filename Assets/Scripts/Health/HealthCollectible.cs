using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private BoxCollider2D boxCollider;
    private Animator animator;
    



    [SerializeField] private AudioClip biteSound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null && playerHealth.currentHealth < 5)
            {
                playerHealth.AddHealth(healthValue);
                SoundManager.instance.PlaySound(biteSound);
                boxCollider.enabled = false;
                animator.SetTrigger("pickedUp");
            }
        }
    }

    private void Deactivate()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void DestroyObject() {
    Destroy(transform.parent.gameObject);

    
}





}