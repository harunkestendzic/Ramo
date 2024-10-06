using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
   
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
  //  private BoxCollider2D boxcol;
   // [SerializeField] private Transform ignorisani_objekt;

    private float lifetime;
    
    public void ActivateProjectile(){
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Awake(){

       // boxcol = GetComponent<BoxCollider2D>();

    }     
    
    
    private void Update(){
        
       // Physics2D.IgnoreCollision(boxcol,ignorisani_objekt.GetComponent<BoxCollider2D>());
       
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.isTrigger) return;
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
    

    
}
