using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargi : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float gravity;
    
    float jumpVelocity = 0f;
    bool isGround = true;
    Rigidbody rb;
    
    void Start() => rb = GetComponent<Rigidbody>();

    void Update(){
        Jump();
        Move();Debug.Log(isGround);
    }

    void Jump(){
        if(!isGround) jumpVelocity -= gravity * Time.deltaTime;
        if(isGround && Input.GetKeyDown(KeyCode.Space)) jumpVelocity = jumpHeight;
    }

    void Move() => rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed,jumpVelocity,Input.GetAxis("Vertical") * speed);

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Floor"){
            isGround = true;
            jumpVelocity = 0;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Floor") isGround = false;
    }

}
