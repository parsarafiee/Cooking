using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingChecker : MonoBehaviour
{
    public bool Impreparing { get; set; }
    public float timer { get; set; }
    public float  TakesTimeToPrepare { get; set; }

    public bool IsPrepared { get; set; }

    public bool IsDoingSomething { get; set; }
    public bool HasSomethingOnhisHand { get; set; }
    private void Start()
    {
        IsDoingSomething = false;
        IsPrepared = false;
    }
    void Update()
    {
        if (Impreparing)
        {
           timer +=Time.deltaTime;
        }
        //if (timer> TakesTimeToPrepare)
        //{
        //    IsPrepared = true;
        //}
        
    }
}
