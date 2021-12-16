using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    Camera mainCamera;
    public static Movement instance;
    public NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        mainCamera = GameObject.FindObjectOfType<Camera>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetAreaCost(1, 3);
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{

        //    RaycastHit rch;
        //    Ray mouseCameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(mouseCameraRay, out rch))
        //    {
        //        navMeshAgent.SetDestination(rch.point);
        //    }
        //}

    }

}
