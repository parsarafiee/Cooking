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
    public Transform trayOfOvenChecker;
    public Transform trayOfMainChef;
    public Transform trayOfToppingChecker;
    public Transform CustomerTable;
    public Transform MainChefLocation;
    public Transform toppingTableTransform;
    public Transform dropBirgerTable;

    public Transform foodParents;


    public GameObject HamburgerPrefab;
    public Transform HaburgerFinishLocation;
}
