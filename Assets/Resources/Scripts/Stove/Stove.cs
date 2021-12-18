using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material material;
    public bool IsFull { get; set; }
    public bool stoveIsOn { get; set; }
    public bool BurgerIsReady { get; set; }
    float BurgerTimer = 0;
    float timeToMakeOneBurger=10;
    public void Refresh()
    {
        if (stoveIsOn)
        {
            BurgerTimer += Time.deltaTime;
          //  Debug.Log(BurgerTimer);
        }
        CheckIFBurgerIsReady();
        //if (BurgerIsReady)
        //{
        //    mesh.material = material;
        //    //Make a Sound
        //    //
        //}

            //Make a Sound
            //
        
    }

    void CheckIFBurgerIsReady()
    {
        if (BurgerTimer> timeToMakeOneBurger)
        {
            BurgerIsReady = true;
        }
    }

    public void SwitchStove()
    {

        stoveIsOn = !stoveIsOn;
        BurgerTimer = 0;
    }




}
