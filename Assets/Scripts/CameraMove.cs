using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float XSpeed;
    public float YSpeed;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        CameraRotate_Mouse();
    }

    private void CameraRotate_Mouse(){

        float y = Input.GetAxis("Mouse Y") * YSpeed;
        //縦回転
        transform.RotateAround(transform.position, transform.right, -y);
    }

}
