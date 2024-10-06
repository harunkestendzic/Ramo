using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    
    private PlayerMovement playerMovement;
    [SerializeField] private float damage;
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate=2f;
    float nextAttackTime=0f;

    [SerializeField] private AudioClip jab;
    [SerializeField] private AudioClip cross;


    private void Awake(){
        anim=GetComponent<Animator>();
        playerMovement=GetComponent<PlayerMovement>();
    }

    void Update()
    {

        //pravi delay izmedju udaraca
        if(Time.time >= nextAttackTime)
        {

        if (Input.GetKeyDown(KeyCode.E) && playerMovement.canAttack())
        {

            //napad funkcija
            Attack();

            //pravi delay izmedju udaraca
            nextAttackTime= Time.time + 1f / attackRate;

        }

        }
        
    }

    void Attack()
    {
        
        //Bira izmedju dvije animacije za udarac
        int randomAttack = Random.Range(0, 2); 
        
  

        if (randomAttack == 0)
        {
            anim.SetTrigger("attack_jab");
            SoundManager.instance.PlaySound(jab);
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<Health>().TakeDamage(damage);
            }

        }
        else
        {
            
            anim.SetTrigger("attack_cross");
            SoundManager.instance.PlaySound(cross);
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<Health>().TakeDamage(damage);
            }
        }

        
    
    }

    void OnDrawGizmosSelected(){
        if(attackPoint==null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

   
}
