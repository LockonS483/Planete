using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    Color oCol;
    Color inactiveCol;
    Color tCol;
    SpriteRenderer r;

    public bool refreshJump;
    public bool refreshDash;
    public bool active;
    PlayerScript p;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        oCol = r.color;
        inactiveCol = oCol;
        inactiveCol.a = 0.2f;
        tCol = oCol;
        active = true;
        p = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!active){
            tCol = inactiveCol;
            if(p.grounded){
                active = true;
            }
        }else{
            tCol = oCol;
        }

        r.color = Color.Lerp(r.color, tCol, 1f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!active) return;
        if(other.gameObject.tag == "Player"){
            p.Refresh(refreshDash, refreshJump);
            active = false;
        }
    }
}
