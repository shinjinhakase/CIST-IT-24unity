using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluecube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            Vector3 v = transform.position;
            v.y += 0.1f;
            transform.position = v;
        }
    }
}
