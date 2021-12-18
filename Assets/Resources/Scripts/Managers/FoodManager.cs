using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType { Salad, Hamburger, Bread, Tomato }

public class FoodManager
{

    #region Singleton
    public static FoodManager Instance
    {
        get
        {
            if (instance == null)
                instance = new FoodManager();
            return instance;
        }
    }


    private static FoodManager instance;

    private FoodManager() { }
    #endregion

    public List<Food> saladList;
    public List<Food> hamburgerList;
    public List<Food> breadList;
    public List<Food> tomatoList;

    int numberOfToppings=GameLinks.gl.NUmberOfToppings;
    public Dictionary<FoodType, GameObject> foodPrefabDict = new Dictionary<FoodType, GameObject>(); //all enemy prefabs

    public void Initialize()
    {
        saladList = new List<Food>();
        hamburgerList= new List<Food>();
        breadList= new List<Food>();
        tomatoList= new List<Food>();

        foreach (FoodType etype in System.Enum.GetValues(typeof(FoodType))) //fill the resource dictionary with all the prefabs
        {
            foodPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Foods/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        SpawnInitialFood();
    }

    public void SpawnInitialFood( )
    {
        CreatFood(GameLinks.gl.sladLocationSpawner.position, FoodType.Salad, GameLinks.gl.NUmberOfToppings, saladList);
        CreatFood(GameLinks.gl.hamburgerLocationSpawner.position, FoodType.Hamburger, GameLinks.gl.NUmberOfToppings, hamburgerList);
        CreatFood(GameLinks.gl.tomatoLocationSpawner.position, FoodType.Tomato, GameLinks.gl.NUmberOfToppings, tomatoList);
        CreatFood(GameLinks.gl.breadLocationSpawner.position, FoodType.Bread, GameLinks.gl.NUmberOfToppings, breadList);

    }
    public void CreatFood(Vector3 pos ,FoodType foodType , int number ,List<Food> list)
    {

        for (int i = 0; i < number; i++)
        {
            GameObject newFood = GameObject.Instantiate(foodPrefabDict[foodType], GameLinks.gl.foodParents);
            Food f = newFood.GetComponent<Food>();
            newFood.AddComponent<Rigidbody>();
            f.Initialize(pos+new Vector3(0,(float)i/2,(float)i/12f));
            list.Add(f);
        }

    }
    public void OneFoodIsPicked(Food food)
    {

        if (food.GetComponent<Food_Bread>())
        {
            breadList.Remove(food);
        }
        else if (food.GetComponent<Food_Beef>())
        {
            hamburgerList.Remove(food);
        }
        else if (food.GetComponent<Food_Tomato>())
        {
            tomatoList.Remove(food);
        }
        else if (food.GetComponent<Food_Salad>())
        {
            saladList.Remove(food);
        }



    }
    public void AddOneFoodTolist(Food food)
    {

        if (food.GetComponent<Food_Bread>())
        {
            breadList.Add(food);
        }
        if (food.GetComponent<Food_Beef>())
        {
            hamburgerList.Add(food);
        }
        if (food.GetComponent<Food_Tomato>())
        {
            tomatoList.Add(food);
        }
        if (food.GetComponent<Food_Salad>())
        {
            saladList.Add(food);
        }



    }
    public void Refresh()
    {
        Debug.Log(breadList.Count);
    }

}
