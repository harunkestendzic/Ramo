using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private bool hit;
    private float lifetime;

    [SerializeField] private float splashRange;

    private AudioSource audioSource;

    [SerializeField] public AudioClip[] audioClips;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateBomb()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (hit) return;

        lifetime += Time.deltaTime;
        if (lifetime > 8) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayRandomSound();

        if (splashRange > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    var player = hitCollider.GetComponent<Health>();
                    if (player)
                    {
                        var closestPoint = hitCollider.ClosestPoint(transform.position);
                        var distance = Vector3.Distance(closestPoint, transform.position);
                        player.TakeDamage(1);
                    }
                }
            }
        }

        hit = true;
        boxCollider.enabled = false;
        body.simulated = false;
        anim.SetTrigger("explode");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRange);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void PlayRandomSound()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
