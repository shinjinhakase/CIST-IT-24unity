using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject goalText;
    public GameObject startText;
    public GameObject title;
    public GameObject button;
    
    float jumpVelocity = 0f;
    bool isGround = true;
    bool isStart = false;
    bool isDropBGM = false;
    
    Rigidbody rb;
    Animator anim;
    AudioSource audio;
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        goalText.SetActive(false);
    }

    void Update(){
        if(isStart){
            Jump();
            LookForward();
            Move();
            if(this.transform.position.y > 70) isDropBGM = true;
        }
        if(this.transform.position.y < -1) this.transform.position = new Vector3(0f,1f,0f);
    }

    public void Button(){
        isStart = true;
        //Canvas.SetActive(false);
        title.SetActive(false);
        startText.SetActive(false);
        button.SetActive(false);
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
            if(other.gameObject.transform.position.y < 1 && isDropBGM){
                audio.PlayOneShot(dropBGM);
                isDropBGM = false;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Goal"){
            Debug.Log("goal");
            goalText.SetActive(true);
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
