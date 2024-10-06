using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leteci : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private bool switc ;
    [SerializeField] private float speed;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private void Awake(){
        body=GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if(switc){
            moveRight();
        }
        if(!switc){
            moveLeft();
        }
        

       
       
    }

    private void moveRight(){
        transform.Translate(speed*Time.deltaTime,0,0);
    }
    private void moveLeft(){
        transform.Translate(-speed*Time.deltaTime,0,0);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="leteci_point_desni"){
             switc = false;
        }
        if(collision.gameObject.tag=="leteci_point_lijevi"){
             switc = true;
        }
    }
}
