using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenChecker : MonoBehaviour
{
    public bool HasTheBeefOnHisHand { get; set; }
    public bool TuredOnTheOven { get; set; }
    private void Start()
    {
        HasTheBeefOnHisHand = false;
    }
}
