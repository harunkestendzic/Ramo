using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    public bool isChasing;
    public float chaseDistance;
    [SerializeField] private Transform enemy;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] private Animator anim;

    private FloatingHealthBar healthBar;
    

    private void Awake()
    {
        initScale = enemy.localScale;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Update()
    {
        if (isChasing)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, chaseDistance, playerLayer);
            if (playerCollider != null)
            {
                Transform player = playerCollider.transform;
                if (transform.position.x > player.position.x)
                {
                    transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    healthBar.transform.localScale = new Vector3(1, 1, 1);
                    anim.SetBool("moving", true);
                }
                else if (transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    healthBar.transform.localScale = new Vector3(-1, 1, 1);
                    anim.SetBool("moving", true);
                }
                
            }
            else
            {
                isChasing = false;
            }
        }
        else
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, chaseDistance, playerLayer);
            if (playerCollider != null)
            {
                isChasing = true;
            }

            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x){
                    MoveInDirection(-1);
                    healthBar.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                    DirectionChange();
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x){
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
        anim.SetBool("moving", false);
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
        initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
        enemy.position.y, enemy.position.z);
    }
}
