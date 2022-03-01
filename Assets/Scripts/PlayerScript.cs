using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : PhysicsObject
{

    public RaycastHit2D ground;
    public float maxSpeed;
    float launch;
    public float dashCooldown;
    public float dashpower;
    public float jumpPower;

    bool frozen = false;

    public bool jumping;

    float fallForgivenessCounter;
    float fallForgiveness;

    bool canDoubleJump;

    int moveDir;
    float cDashCooldown;
    Vector3 oLocalScale;
    Vector3 dashLocalScale;
    public Transform scalepoint;

    // Start is called before the first frame update
    void Start()
    {
        launch = 0;
        frozen = false;
        moveDir = 1;

        oLocalScale = transform.localScale;
        dashLocalScale = oLocalScale;
        dashLocalScale.y *= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        ComputeVelocity();

        if(cDashCooldown > 0){
            cDashCooldown -= Time.deltaTime;
        }
    }

    protected void ComputeVelocity(){
        Vector2 move = Vector2.zero;
        ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up);

        
        if(Mathf.Abs(launch) >= 0.11f){
            ScaleAround(transform, scalepoint, Vector3.Lerp(transform.localScale, dashLocalScale, 1f));
            move.x = launch;
            launch -= (launch / Mathf.Abs(launch)) * Time.deltaTime * 3.5f;
        }else{
            ScaleAround(transform, scalepoint, Vector3.Lerp(transform.localScale, oLocalScale, 1f));
            move.x = Input.GetAxis("Horizontal") + launch;
            launch = 0;
        }

        if(frozen) return;
        
        if(Input.GetButtonDown("Jump")){
            //print("JUMPP");
            if(grounded && !jumping){
                Jump(1f);
            }else if(canDoubleJump){
                Jump(1f);
                canDoubleJump = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            if(cDashCooldown <= 0){
                launch = moveDir * dashpower;
                cDashCooldown = dashCooldown;
            }
        }

        if(move.x > 0.01f){
            //maybe flip scale
            moveDir = 1;
        }else if (move.x < -0.01f){
            //maybe flip scale
            moveDir = -1;
        }

        if(!grounded){
            if(fallForgivenessCounter < fallForgiveness && !jumping){
                fallForgivenessCounter += Time.deltaTime;
            }else{
                grounded = false;
            }
        }else{
            if(jumping) jumping = false;
            canDoubleJump = true;
        }

        //print(move);
        targetVelocity = move * maxSpeed;
    }

    protected void Jump(float multi){
        Debug.Log("JUMP");
        if(velocity.y != jumpPower){
            velocity.y = jumpPower * multi;
            jumping = true;
        }
    }

    public static void ScaleAround(Transform target, Transform pivot, Vector3 scale) {
        Transform pivotParent = pivot.parent;
        Vector3 pivotPos = pivot.position;
        pivot.parent = target;        
        target.localScale = scale;
        target.position += pivotPos - pivot.position;
        pivot.parent = pivotParent;
     }
}