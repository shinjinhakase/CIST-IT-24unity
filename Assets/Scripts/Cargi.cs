using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargi : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float gravity;
    public float camSpeed;

    public GameObject camera;
    
    float jumpVelocity = 0f;
    bool isGround = true;
    Rigidbody rb;
    
    void Start() => rb = GetComponent<Rigidbody>();

    void Update(){
        Jump();
        LookForward();
        Move();
    }

    void Jump(){
        if(!isGround) jumpVelocity -= gravity * Time.deltaTime;
        if(isGround && Input.GetKeyDown(KeyCode.Space)) jumpVelocity = jumpHeight;
    }

    void LookForward(){
        float x = Input.GetAxis("Mouse X") * camSpeed;
        transform.RotateAround(transform.position, Vector3.up, x);
    }

    void Move(){
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float z = Input.GetAxisRaw("Vertical") * speed;
        Vector3 comFoward = new Vector3(transform.forward.x,jumpVelocity,transform.forward.z).normalized;
        Vector3 velocityVector = comFoward * z + transform.right * x;
        rb.velocity = new Vector3 (velocityVector.x,jumpVelocity,velocityVector.z);
    }

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
