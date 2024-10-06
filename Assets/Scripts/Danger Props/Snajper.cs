using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snajper : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private Health playerHealth;

    private Animator anim;

    private bool isWorking;
    [SerializeField] private float cooldown;
    private float cooldownTimer;

    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0){
            if (PlayerInSight()){
                playerHealth.TakeDamage(5);
            }
            isWorking = true;
            cooldownTimer = cooldown;

            StartCoroutine(DisableAfterOneSecond());
        }
    }

    private IEnumerator DisableAfterOneSecond() {
        yield return new WaitForSeconds(0.2f);
        isWorking = false;
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            playerLayer
        );

        if (hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos(){
        if (boxCollider == null){
            boxCollider = GetComponent<BoxCollider2D>();
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }
}
