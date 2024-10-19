using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float XSpeed;
    public float YSpeed;

    bool isStart = false;

    void Start(){

    }

    void Update(){
        if(isStart){
            CameraRotate_Mouse();
        }
    }

    private void CameraRotate_Mouse(){

        float y = Input.GetAxis("Mouse Y") * YSpeed;
        //縦回転
        transform.RotateAround(transform.position, transform.right, -y);
    }

    public void Button(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isStart = true;
    }
}
