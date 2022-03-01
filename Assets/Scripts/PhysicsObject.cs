using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [Header ("Physics")]
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    public Vector2 targetVelocity;
    public bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;

    public Vector2 velocity;
    [SerializeField] protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start(){
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        Debug.Log("huh");
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        Debug.Log("huh");
    }

    void FixedUpdate()
    {
        //Debug.Log("Hello i am physics object update");
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if(distance <= minMoveDistance) return;

        int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        hitBufferList.Clear();

        for (int i=0; i < count; i++){
            PlatformEffector2D plat = hitBuffer[i].collider.GetComponent<PlatformEffector2D>();

            if(!plat || (hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement)){
                hitBufferList.Add(hitBuffer[i]);
            }
        }

        for (int i=0; i<hitBufferList.Count; i++){
            Vector2 currentNormal = hitBufferList[i].normal;
            if(currentNormal.y > minGroundNormalY)
            {
                grounded = true;
                if(yMovement){
                    groundNormal = currentNormal;
                    currentNormal.x = 0;
                }
                print(currentNormal.y);
            }

            float proj = Vector2.Dot(velocity, currentNormal);
            if(proj < 0){
                velocity = velocity - proj * currentNormal;
            }

            float modifiedDistance = hitBuffer[i].distance - shellRadius;
            distance = modifiedDistance < distance ? modifiedDistance : distance;
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
