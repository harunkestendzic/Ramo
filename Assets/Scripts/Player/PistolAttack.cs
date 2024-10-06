using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] metci;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer= Mathf.Infinity;

   [SerializeField] public int maxAmmo = 3 ;
   [SerializeField] public int currentAmmo = 3;

   [SerializeField] private AudioClip pistolSound;


   public Transform shotText;

    private void Awake(){
        anim=GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        shotText.GetComponent<Text>().text= currentAmmo.ToString();
      //  shotText = GetComponent<Text>();
    }

   private void Update(){

   
    shotText.GetComponent<Text>().text= currentAmmo.ToString();

    if(Input.GetKey(KeyCode.F) && cooldownTimer > attackCooldown && playerMovement.canAttack() && currentAmmo>0)
        Attack();

    cooldownTimer += Time.deltaTime;

   }

   private void Attack(){


    anim.SetTrigger("pistol_shoot");
    SoundManager.instance.PlaySound(pistolSound);
    cooldownTimer=0;

    currentAmmo--;

    metci[FindMetak()].transform.position= firePoint.position;
    metci[FindMetak()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

   }

   public void Reload(){
    currentAmmo=maxAmmo;
   }

   private int FindMetak(){

    for(int i=0;i<metci.Length;i++){
        if(!metci[i].activeInHierarchy)
            return i;
    }
    return 0;

   }

   
}
