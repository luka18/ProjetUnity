using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RB2 : MonoBehaviour
{
    
    Rigidbody Myrigidbody;
    float vertical;
    float horizontal;
    public float speed = 5.0f;
    public float sprintspeed = 7.5f;
    public float JumpHeight = 20.0f;
    private float jump = 0.0f;
    private bool slowdown = false;
    private Vector3 forcetoadd;
    private Vector3 desiredmove;
    
    
    // LA VISION
    private float rotLR;
    private float Mousespeed = 5.0f;
    private float upDownRange = 60.0f; // Maximum range to go from the middle to the bottom/top 
    private float verticalRotation;
    private Transform cam;
    private Transform Head;
    private Transform Robot;
    private bool ff = false;


    //GROUNDCHECK
    public static bool grounded = false;
    private BoxCollider box1, box2;
    private bool cangoup = true;
    public int maxSlope = 65;
    //CROUCHING
    public static bool Crouched = false;
    private float deltat = 0;
    private bool goinup = false;
    private float timetogoup = 0;

    //Animation
    Animator anim;

    //Shootin
    
    //LES BOUTONS
    public List<GameObject> ButtonList = new List<GameObject>();


    private BoxCollider Mycollider;
   
    // Use this for initialization
    void Start()
    {
        Myrigidbody = GetComponent<Rigidbody>();
        cam = transform.FindChild("Camera");
        Mycollider = GetComponent<BoxCollider>();
        Robot = transform.Find("IDDLE OFI");
        print("one");
        //anim = transform.FindChild("IDDLE OFI").GetComponent<Animator>();

        //Head = transform.Find("Iddle4").transform.Find("joint7").transform.Find("joint4").transform.Find("mnr_120:mnr_120:neck").transform.Find("polySurface104").GetComponent<Transform>();
        print("two");
        

    }

    // Update is called once per frame
    void Update()
    {
       
        Mouselook();



        if (Input.GetButtonDown("Jump") & grounded & (!Crouched))
        {
            jump = JumpHeight;
        } 


        if (Input.GetButton("Crouch"))
        {
            
            
            
            Crouched = true;
            Mycollider.size = new Vector3(1, 2.0f, 1);
           
            if (grounded)
            {
                speed = 2.5f;
            }
            
            
            
        }

        if (!(Input.GetButton("Crouch"))&& Crouched)
        {
            if (HeadTtrigger.goup && grounded)
            {
               
                //transform.Translate(0, 0.25f, 0);
                
                speed = 5.0f;
                //Mycollider.size = new Vector3(1, 2.5f, 1);
                Crouched = false;
                goinup = true;
            }
        }

        if(goinup)
        {
            deltat += Time.deltaTime*1.2f;
            
            Mycollider.size = Vector3.Lerp(new Vector3(1, 2,1), new Vector3(1, 2.5f, 1), deltat);
            

            if (deltat >1)
            {
                deltat = 0;
                goinup = false;
            }
                

        }



        if (speed == 10)
        {
            if (horizontal != 0 & vertical>0)
                speed = 5.0f;
        }
      
        if (Input.GetButton("Sprint"))
        {
            if(grounded & vertical > 0 & horizontal == 0& !Crouched)
                speed = sprintspeed;
           
          
        }
        if (Input.GetButtonUp("Sprint") || slowdown) 
        {
            
                speed = 5.0f;
                slowdown = false;
           
        }

       

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("draw");
            RaycastHit hit;
            Debug.DrawRay(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward*3, Color.black, 1.0f);
            if ((Physics.Raycast(transform.position + new Vector3(0, 2.0f, 0), cam.transform.forward, out hit,3.0f)))
            {
                

               
             
                if (hit.transform.name == "Button")
                {

                    //
                }
                
                if (hit.transform.name == "Button2") 
                {
                  
                    //cubesalive.Gogocubes = true;
                }
                if(hit.transform.name == "Button3")
                {

                    //
                }



            }
            

        }


        // les variable à update chaque frame

    }
    void FixedUpdate()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (grounded)
        {
            desiredmove = (transform.forward * vertical + transform.right * horizontal).normalized * speed;
            forcetoadd = new Vector3(desiredmove.x - Myrigidbody.velocity.x, jump, desiredmove.z - Myrigidbody.velocity.z);
            Myrigidbody.AddForce(forcetoadd, ForceMode.Impulse);
            print("groundded");
            
        }
        
        else //if (speed == 5.0f)
        {
            desiredmove = (transform.forward * vertical + transform.right * horizontal ).normalized * speed  ;
            forcetoadd = new Vector3(desiredmove.x - Myrigidbody.velocity.x, jump, desiredmove.z - Myrigidbody.velocity.z)/5;
            Myrigidbody.AddForce(forcetoadd, ForceMode.Impulse);
            print("in jump");
        }

      
        jump = 0.0f;
        // les debug vecteur movement
        Debug.DrawRay(desiredmove + new Vector3(0, 1, 0), forcetoadd, Color.white, 1.0f);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), desiredmove, Color.green, 1.0f);
        Debug.DrawRay(Myrigidbody.transform.position + new Vector3(0, 1, 0), Myrigidbody.velocity, Color.red, 1.0f);



    }
    void Mouselook()
    {
        float rotLeftRight = Input.GetAxis("Mouse X") * Mousespeed;
        transform.Rotate(0, rotLeftRight, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * Mousespeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
   
        
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        if (grounded && !Crouched)
        {
            
            
            //Robot.localRotation = Quaternion.Euler(0, 0, -verticalRotation / 15);
        }
           


    }

   
    

    void OnCollisionStay(Collision coll)
    {
        foreach(ContactPoint contact in coll.contacts)
        {
            if(Vector3.Angle(contact.normal,Vector3.up)<maxSlope)
            {
                grounded = true;
                
                
            }
        }
    }
    void OnCollisionExit()
    {
        grounded = false;
    }
    



    
}


    
    