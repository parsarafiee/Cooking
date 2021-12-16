using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{

    public static bool CheckIfAvailabeStove()
    {
        foreach (var gameObject in GameLinks.gl.allStovesLocations)
        {
            if (gameObject.GetComponent<Stove>().IsFull == false)
            {
                return true;
            }
        }
        return false;
    }

    public static bool CheckDistance(Transform pos1, Transform pos2, float variation)
    {
        if (Vector3.Distance(pos1.position, pos2.position) < variation)
        {
            return true;
        }
        return false;
    }
    public static GameObject FindAvailableStove()
    {
        GameObject tr = null;
        if (CheckIfAvailabeStove())
        {
            foreach (var gameobject in GameLinks.gl.allStovesLocations)
            {
                if (!gameobject.GetComponent<Stove>().IsFull)
                {
                    return gameobject;
                }
            }

        }
        return tr;
    }

    public static void PickUpOneFoodObjectOfList(List<Food> list)
    {
        GameObject.Destroy(list[0].gameObject);
        FoodManager.Instance.OneFoodIsPicked(list[0]);
    }

    public static GameObject WhichBeefReadyToPicked()
    {
        GameObject obj = null;
        foreach (GameObject item in GameLinks.gl.allStovesLocations)
        {
           if(item.GetComponent<Stove>().BurgerIsReady && item.GetComponent<Stove>().stoveIsOn)

            {
                return item;
            }
        }
        return obj;

    }





}
