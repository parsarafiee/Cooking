using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public virtual void Initialize(Vector3 pos)
    {
        SetPosition(pos);

    }

    public virtual void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

}
