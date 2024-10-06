using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistoljPickup : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;

    [SerializeField] private AudioClip klikSound;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PistolAttack pistolAttack = collision.GetComponent<PistolAttack>();
            if (pistolAttack != null && pistolAttack.currentAmmo < pistolAttack.maxAmmo)
            {
                animator.SetTrigger("pickedUp");
                SoundManager.instance.PlaySound(klikSound);
                boxCollider.enabled = false;
                pistolAttack.Reload();
            }
        }
    }

    private void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
