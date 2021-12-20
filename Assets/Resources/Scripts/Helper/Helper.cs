using System;
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
        Food f = list[0];
        FoodManager.Instance.OneFoodIsPicked(f);
        GameObject.Destroy(f.gameObject);
    }
    public static void PickUpFoodsOfList(List<Food> list, int Number)
    {
        
        for (int i = Number - 1; i >= 0; i--)
        {
            Food f = list[0];

            FoodManager.Instance.OneFoodIsPicked(f);
            GameObject.Destroy(f.gameObject);
        }
    }
    public static List<Food> GiveTypeList(GameObject food)
    {
        List<Food> list = null;
        if (food.GetComponent<Food_Beef>())
        {
            list = FoodManager.Instance.hamburgerList;
        }
        if (food.GetComponent<Food_Bread>())
        {
            list = FoodManager.Instance.breadList;
        }
        if (food.GetComponent<Food_Tomato>())
        {
            list = FoodManager.Instance.tomatoList;
        }
        if (food.GetComponent<Food_Salad>())
        {
            list = FoodManager.Instance.saladList;
        }
        return list;
    }
    public static Transform GiveTheToppingLocation(Food food)
    {
        Transform g = null;
        if (food.gameObject.GetComponent<Food_Beef>())
        {
            g = GameLinks.gl.hamburgerLocation;
        }
        if (food.gameObject.GetComponent<Food_Bread>())
        {
            g = GameLinks.gl.breadLocation;
        }
        if (food.gameObject.GetComponent<Food_Tomato>())
        {
            g = GameLinks.gl.tomatoLocation;
        }
        if (food.gameObject.GetComponent<Food_Salad>())
        {
            g = GameLinks.gl.sladLocation;
        }
        return g;
    }
    public static GameObject WhichBeefReadyToPicked()
    {
        GameObject obj = null;
        foreach (GameObject item in GameLinks.gl.allStovesLocations)
        {
            if (item.GetComponent<Stove>().BurgerIsReady && item.GetComponent<Stove>().stoveIsOn)

            {
                return item;
            }
        }
        return obj;

    }
    public static GameObject WhichBeefReadyToPickedForChef()
    {
        GameObject obj = null;
        foreach (GameObject item in GameLinks.gl.allStovesLocations)
        {
            if (item.GetComponent<Stove>().BurgerIsReady)

            {
                return item;
            }
        }
        return obj;

    }

    public static bool CheckNumberOfToppings(List<Food> foodListInfo, int MaxToppingNumber)
    {
        bool isFull = false;
        if (foodListInfo.Count == MaxToppingNumber)
        {
            isFull = true;
        }
        return isFull;
    }

    internal static void AddOneFoodObjectOfList(List<Food> list)
    {
        if (list ==FoodManager.Instance.breadList)
        {
            FoodManager.Instance.CreatOneFood(FoodType.Bread,list);

        }
        if (list == FoodManager.Instance.tomatoList)
        {
            FoodManager.Instance.CreatOneFood(FoodType.Tomato, list);

        }
        if (list == FoodManager.Instance.hamburgerList)
        {
            FoodManager.Instance.CreatOneFood(FoodType.Hamburger, list);


        }
        if (list == FoodManager.Instance.saladList)
        {
            FoodManager.Instance.CreatOneFood(FoodType.Salad, list);

        }
    }
}
