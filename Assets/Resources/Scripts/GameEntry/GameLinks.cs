using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLinks : MonoBehaviour
{
    public static GameLinks gl;
    public GameObject HamburgerMakerLocation;
    public Transform sladLocation;
    public Transform hamburgerLocation;
    public Transform breadLocation;
    public Transform tomatoLocation;

    public Transform sladLocationSpawner;
    public Transform hamburgerLocationSpawner;
    public Transform breadLocationSpawner;
    public Transform tomatoLocationSpawner;
    public int NUmberOfToppings;
    public int SaladnUmbers;

    public List<GameObject> allStovesLocations;
    public Transform HeadOfOvenChecker;
    public Transform CustomerTable;

}
