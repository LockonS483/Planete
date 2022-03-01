using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform pt;
    PlayerScript p;
    public Vector3 offset;
    public float followSpeed;
    Vector3 targetV;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(p.dashing){
            offset.y = 0.25f;
        }else{
            offset.y = 0;
        }
        targetV = pt.position + offset;
        //transform.position = Vector3.Slerp(transform.position, targetV, followSpeed);
    }

    void FixedUpdate(){
        transform.position = targetV;
    }
}
