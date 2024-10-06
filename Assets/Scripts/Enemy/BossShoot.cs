using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    
    private float timer;
    private float interval;
    public GameObject player;

    private bool canShoot = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetRandomInterval();
    }

    private void Update()
    {
        if (!canShoot)
        {
            return;
        }

       // float distance = Vector2.Distance(transform.position, player.transform.position);

        
            timer += Time.deltaTime;
            if (timer > interval)
            {
                timer = 0;
                shoot();
                SetRandomInterval();
            }
        
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void SetRandomInterval()
    {
        interval = Random.Range(1.1f, 2.1f);
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    public void StartShooting()
    {
        canShoot = true;
    }
}


   
