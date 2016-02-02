using UnityEngine;
using System.Collections;

public class HeadTtrigger : MonoBehaviour {
    private int OnMyhead = 0;
    private bool opti = false;
    public static bool goup = true;
    BoxCollider boxcol;

    // Use this for initialization
    void Start () {
        boxcol = GetComponent<BoxCollider>();
	
	}
    void Update()
    {
        if (RB2.Crouched)
        {
            boxcol.center = new Vector3(0, 0.5f, 0);
            opti = true;
        } 
        else if (opti)
        {
            boxcol.center = new Vector3(0, 0.0f, 0);
            opti = false;
        }
    }
    
    void OnTriggerEnter()
    {
        OnMyhead += 1;
        goup = false;
        print(OnMyhead);
    }
    void OnTriggerExit()
    {
        OnMyhead -= 1;
        print(OnMyhead);
        if (OnMyhead == 0)
            goup = true;

    }
	
}
