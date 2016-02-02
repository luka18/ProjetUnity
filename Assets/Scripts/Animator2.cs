using UnityEngine;
using System.Collections;

public class Animator2 : MonoBehaviour
{
    private Animator anim;
    private bool jumping = false;
    private bool carrying = false;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (jumping && RB2.grounded)
        {
            anim.Play("Landing");
            jumping = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.Play("Jump");
            jumping = true;
        }
        
        /*if (Input.GetMouseButtonDown(1))
        {

                if (carrying)
                {
                    anim.Play("Throwit");
                    carrying = false;
                }
                else
                {
                    anim.Play("Gotit");
                    carrying = true;
                }
            
        }*/
        

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("Push");
        }
        if (Input.GetButtonDown("Crouch"))
        {
            anim.Play("Crouching");
        }
        if (Input.GetButtonUp("Crouch"))
        {
            anim.Play("Getup");
        }




    }
    
}
