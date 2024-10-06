using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private RespawnEnemy resEnemy;
    public float currentHealth { get; private set; }

    private Animator anim;
    public bool dead;

    //enemy healthbar
    private FloatingHealthBar healthBar;

    [SerializeField] private AudioClip HurtSound1;
    [SerializeField] private AudioClip HurtSound2;
    [SerializeField] private AudioClip DeathSound;
   
                

    

    private void Awake()
    {
        currentHealth = startingHealth;
        anim= GetComponent<Animator>();
        resEnemy= GetComponent<RespawnEnemy>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void Start(){
         if (gameObject.CompareTag("Enemy")){
        healthBar.UpdateHealthBar(currentHealth,startingHealth);
         }

    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
         if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")){
        healthBar.UpdateHealthBar(currentHealth,startingHealth);
         }
        

        if(currentHealth > 0)
        {
            //hurt animacija
            anim.SetTrigger("hurt");

            //hurt zvukovi
            AudioClip selectedSound = Random.value > 0.5f ? HurtSound1 : HurtSound2;
            SoundManager.instance.PlaySound(selectedSound);
        }
        else
        {
            if(!dead)
            {

            //PLAYER

            //Ne moze se micat vise player
            if (GetComponent<PlayerMovement>() != null){
            GetComponent<PlayerMovement>().enabled=false;
            }
            if (GetComponent<MeleeAttack>() != null){
            GetComponent<MeleeAttack>().enabled=false;
            }
            if (GetComponent<PistolAttack>() != null){
            GetComponent<PistolAttack>().enabled=false;
            }
            //zvukovi
            
            SoundManager.instance.PlaySound(DeathSound);
            

            //animacije
             if (gameObject.CompareTag("Player")){
            anim.SetBool("grounded",true);
            anim.SetBool("crouch",false);
            anim.SetBool("crouch_walking",false);
            anim.SetBool("climbing",false);
            anim.SetBool("climbing_moving",false);
            }
            
            //ENEMY
            
            if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")){
            healthBar.gameObject.SetActive(false);
            }

            //zakoci neprijateljski napad
            if (GetComponent<MeleeEnemy>() != null){
            GetComponent<MeleeEnemy>().enabled=false;
            }

            //zakoci ?enemy? rigidbody da ne propada
            if (GetComponent<Rigidbody2D>() != null ) {
                GetComponent<Rigidbody2D>().simulated = false;
            }

            //zakoci neprijateljski patrol
            if (GetComponentInParent<EnemyMovement>() != null){
            GetComponentInParent<EnemyMovement>().enabled=false;
            }
            if (GetComponentInParent<EnemyMovementBezChase>() != null){
            GetComponentInParent<EnemyMovementBezChase>().enabled=false;
            }
            if (gameObject.CompareTag("Enemy") && GetComponent<RangedEnemy>() == null)
            {
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("DestroyObjectParent", 4f);
            }
            else if (gameObject.CompareTag("Enemy") && GetComponent<RangedEnemy>() != null)
            {
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("DestroySecondParent", 4f);
            }
            
            
            //ZA SVE
            anim.SetTrigger("die");
            dead = true;

            }
        }
    }

    public void Respawn(){
        dead = false;
        AddHealth(startingHealth);
        GetComponent<Rigidbody2D>().simulated = true;
        anim.ResetTrigger("die");
        anim.Play("idle");

        if (GetComponent<PlayerMovement>() != null){
            GetComponent<PlayerMovement>().enabled=true;
            }
        if (GetComponent<MeleeAttack>() != null){
            GetComponent<MeleeAttack>().enabled=true;
            }
        if (GetComponent<PistolAttack>() != null){
            GetComponent<PistolAttack>().enabled=true;
            }
        

    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth+ _value,0,startingHealth);
    }

   private void Deactivate(){
        gameObject.SetActive(false);
   }
   private void DestroyObjectParent() {
    Destroy(transform.parent.gameObject);
    }
   private void DestroySecondParent() {
    Transform secondParent = transform.parent?.parent;
    if (secondParent != null) {
        Destroy(secondParent.gameObject);
    } else {
        Debug.LogWarning("Second parent does not exist.");
    }
}

}
