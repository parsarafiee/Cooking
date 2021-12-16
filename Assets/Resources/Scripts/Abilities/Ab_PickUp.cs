using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_PickUp : MonoBehaviour
{
    BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag  == "Bread")
        {

        }
        
    }

}
