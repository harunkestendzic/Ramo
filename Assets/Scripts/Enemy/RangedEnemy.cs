using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;


    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] metci;


    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private EnemyMovementBezChase enemyMovement;

    [SerializeField] private AudioClip pistolSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovement = GetComponentInParent<EnemyMovementBezChase>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
     
        if(cooldownTimer >= attackCooldown)
        {
            cooldownTimer=0;
            anim.SetTrigger("rangedAttack");
        }
        }

        if(enemyMovement != null)
            enemyMovement.enabled = !PlayerInSight();

    }


    private void RangedAttack(){
        cooldownTimer=0;
        SoundManager.instance.PlaySound(pistolSound);
        attackCooldown = Random.Range(0.4f, 1.2f);
        metci[FindMetak()].transform.position=firepoint.position;
        metci[FindMetak()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int FindMetak(){
        for(int i=0;i<metci.Length; i++){
            if (!metci[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
        Physics2D.BoxCast(boxCollider.bounds.center+ transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y,boxCollider.bounds.size.z),
        0,Vector2.left,0,playerLayer);


        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range*transform.localScale.x *colliderDistance ,
        new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y,boxCollider.bounds.size.z));

    }

}
