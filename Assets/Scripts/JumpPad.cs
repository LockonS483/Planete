using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    PlayerScript p;
    public float power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        p = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            if(p.dashing){
                p.CancelDash();
            }
            p.JumpPad(power);
        }
    }
}
