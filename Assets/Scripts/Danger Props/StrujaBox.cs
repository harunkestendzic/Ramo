using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrujaBox : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private Health playerHealth;

    private Animator anim;

    private bool isWorking;
    [SerializeField] private float cooldown;
    private float cooldownTimer;

    [SerializeField] private AudioClip strujaSound;

    private void Awake(){
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update(){
    cooldownTimer -= Time.deltaTime;

    if (isWorking && PlayerInSight())
    {
        playerHealth.TakeDamage(5);
    }

    if (cooldownTimer < 0){
        isWorking = !isWorking;
        cooldownTimer = cooldown;

        if (!isWorking)
        {
            audioSource.Stop();
        }
    }

    if (isWorking && !audioSource.isPlaying)
    {
        audioSource.clip = strujaSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    anim.SetBool("isWorking", isWorking);
}


     private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            playerLayer
        );

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }
}
