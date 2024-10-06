using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{
    [SerializeField] private Transform bomblay;
    [SerializeField] private GameObject[] bombPrefab;

    public float minTimeBetweenShots = 1f;
    public float maxTimeBetweenShots = 3f;

    private float nextShotTime;

    private void Start()
    {
        nextShotTime = Time.time + Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
/*
    private void Shoot()
    {
        int bombIndex = FindBomb();
        if (bombIndex >= 0) 
        {
            bombPrefab[bombIndex].transform.position = bomblay.position;
            bombPrefab[bombIndex].SetActive(true);
        Rigidbody2D bombBody = bombPrefab[bombIndex].GetComponent<Rigidbody2D>();
        if (bombBody != null) {
            bombBody.simulated = true;
        }
        BoxCollider2D bombCollider = bombPrefab[bombIndex].GetComponent<BoxCollider2D>();
        if (bombCollider != null) {
            bombCollider.enabled = true;
        }
     
        }
    }*/

    private void Shoot()
{
    int bombIndex = FindBomb();
    if (bombIndex >= 0) 
    {
        
        bombPrefab[bombIndex].transform.position = bomblay.position;

       
        bombPrefab[bombIndex].transform.localScale = new Vector3(0.2f, 0.2f, 1f); 
        bombPrefab[bombIndex].transform.rotation = Quaternion.Euler(0, 0, 270f);

       
        bombPrefab[bombIndex].SetActive(true);

        
        Rigidbody2D bombBody = bombPrefab[bombIndex].GetComponent<Rigidbody2D>();
        if (bombBody != null)
        {
            bombBody.simulated = true;
        }

       
        BoxCollider2D bombCollider = bombPrefab[bombIndex].GetComponent<BoxCollider2D>();
        if (bombCollider != null)
        {
            bombCollider.enabled = true;
        }
        
    }
}




    private int FindBomb()
    {
        for (int i = 0; i < bombPrefab.Length; i++)
        {
            if (!bombPrefab[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0; 
    }
}
