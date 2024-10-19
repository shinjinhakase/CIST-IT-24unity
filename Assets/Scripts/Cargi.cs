using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargi : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float gravity;
    public float camSpeed;

    public GameObject camera;
    public GameObject Canvas;
    public AudioClip jumpSound;
    public AudioClip footsteps;
    public AudioClip dropBGM;
    
    float jumpVelocity = 0f;
    bool isGround = true;
    bool isStart = false;
    bool isdropBGM = false;
    
    Rigidbody rb;
    Animator anim;
    AudioSource audio;
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update(){
        if(isStart){
            Jump();
            LookForward();
            Move();
        }
    }

    public void Button(){
        isStart = true;
        Canvas.SetActive(false);
    }

    void Jump(){
        if(!isGround) jumpVelocity -= gravity * Time.deltaTime;
        if(isGround && Input.GetKeyDown(KeyCode.Space)) {
            jumpVelocity = jumpHeight;
            audio.PlayOneShot(jumpSound);
        }
    }

    void LookForward(){
        float x = Input.GetAxis("Mouse X") * camSpeed;
        transform.RotateAround(transform.position, Vector3.up, x);
    }

    void Move(){
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float z = Input.GetAxisRaw("Vertical") * speed;
        Vector3 comFoward = new Vector3(transform.forward.x,0f,transform.forward.z).normalized;
        Vector3 velocityVector = comFoward * z + transform.right * x;
        rb.velocity = new Vector3(velocityVector.x,jumpVelocity,velocityVector.z);
        anim.SetFloat("velocity",new Vector3(velocityVector.x,0f,velocityVector.z).magnitude);
        //audio.PlayOneShot(footsteps);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Floor"){
            isGround = true;
            jumpVelocity = 0;
            anim.SetBool("GetGround",true);
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Floor"){
            isGround = false;
            anim.SetBool("GetGround",false);
            anim.SetTrigger("Jump");
        }
    }

}
