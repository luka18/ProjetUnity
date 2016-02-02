using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    void Start()
    {
        
        if(!isLocalPlayer)
        {
            for (int i =0; i <componentsToDisable.Length; i++)
            {
                print("mdr");
                print(componentsToDisable[i].name);
                componentsToDisable[i].enabled = false;
            }
            
            
            
        }
        else
        {
            print("inst local player");
            sceneCamera = GameObject.Find("FirstView").GetComponent<Camera>();
            print(sceneCamera);
            if (sceneCamera!=null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

    }

    void OnDisable()
    {
        print("OnDisable");
        sceneCamera.gameObject.SetActive(true);
    }

}
