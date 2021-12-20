using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChef : MonoBehaviour
{
    public bool HasBread { get; set; }
    public bool HasTomato { get; set; }
    public bool HasSalad { get; set; }
    public bool HasBeef { get; set; }
    public bool hasTHeHamBurger;
    public bool ImDoneWithTheORder { get; set; }
    private void Start()
    {
        HasBread = false;
        HasTomato = false;
        HasSalad = false;
        HasBeef = false;
        ImDoneWithTheORder = false;
        hasTHeHamBurger = false;
    }
    public void  ResetChef()
    {
        HasBread = false;
        HasTomato = false;
        HasSalad = false;
        HasBeef = false;
        ImDoneWithTheORder = false;
        hasTHeHamBurger = false;

    }
}
