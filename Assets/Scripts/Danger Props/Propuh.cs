using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propuh : MonoBehaviour
{
    [SerializeField] private float minAttackCooldown; 
    [SerializeField] private float maxAttackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;
    private float attackCooldown;

    private void Start()
    {
        
        attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
    }

    private void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<PropuhProjectile>().ActivateProjectile();

        
        attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
