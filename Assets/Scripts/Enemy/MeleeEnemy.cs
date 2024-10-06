using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

// body.simulated = false; PROBAJ OVO
//  boxCollider.enabled = false;
          

    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private EnemyMovement enemyMovement;

    [SerializeField] private AudioClip jab;
    [SerializeField] private AudioClip cross;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
     
        if(cooldownTimer >= attackCooldown)
        {
            cooldownTimer=0;
            

        int randomAttack = Random.Range(0, 2); 
        if (randomAttack == 0)
        {
            anim.SetTrigger("meleeAttack1");
            SoundManager.instance.PlaySound(jab);
        }
        else
        {
            anim.SetTrigger("meleeAttack2");
            SoundManager.instance.PlaySound(cross);
        }


        }
        }

        if(enemyMovement != null)
            enemyMovement.enabled = !PlayerInSight();

    }

private bool PlayerInSight()
{
    RaycastHit2D hit = 
    Physics2D.BoxCast(boxCollider.bounds.center+ transform.right*range*transform.localScale.x*colliderDistance,
    new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y,boxCollider.bounds.size.z),
    0,Vector2.left,0,playerLayer);

    if(hit.collider != null)
        playerHealth = hit.transform.GetComponent<Health>();

    return hit.collider != null;
}

private void OnDrawGizmos()
{
    Gizmos.color=Color.red;
    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range*transform.localScale.x *colliderDistance ,
    new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y,boxCollider.bounds.size.z));

}

private void DamagePlayer(){
    if(PlayerInSight()){
        playerHealth.TakeDamage(damage);
    }
}



}
