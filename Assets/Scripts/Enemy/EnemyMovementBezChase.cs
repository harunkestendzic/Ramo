using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBezChase : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;

    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleDuration;
    private float idleTimer;
    private FloatingHealthBar healthBar;

    [SerializeField] private Animator anim;

    private Health health;

    private void Awake()
    {
        initScale = enemy.localScale;
        health = GetComponent<Health>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void Update()
    {

        if(!health.dead){

        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
                healthBar.transform.localScale = new Vector3(1, 1, 1);
            }
            else
                DirectionChange();
        }
        else{
            if(enemy.position.x <= rightEdge.position.x){
                MoveInDirection(1);
                healthBar.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
                DirectionChange();
        }
        }
        
        
    }

    private void OnDisable()
    {
        anim.SetBool("moving",false);
    }

    private void DirectionChange(){

        anim.SetBool("moving",false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction){

        idleTimer = 0;
        anim.SetBool("moving",true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction,
        initScale.y,initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction *speed,
        enemy.position.y,enemy.position.z);
    }

}
