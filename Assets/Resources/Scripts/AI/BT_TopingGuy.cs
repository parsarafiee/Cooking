using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;

public class BT_TopingGuy : MonoBehaviour
{
    Movement move;

    BT Seq_CheckBread;
    BT Seq_CheckBeef;
    BT Seq_CheckTomato;
    BT Seq_CheckSalad;
    BT Sel_ToppingGuy;
    BT Seq_ImdoingSomething;
    BT Sel_ImdoingSomething;

    ToppingChecker ToppingChecker;

    public int MaxNumber = 5;
    public int maxBeefNUmber = 5;
    public int maxTomatoNUmber = 5;
    public int maxSaladNUmber = 5;

    public float checkDistanceVariation;

    private void Start()
    {
        ToppingChecker = GetComponent<ToppingChecker>();
        move = GetComponent<Movement>();

        Seq_ImdoingSomething = new BT(NODE_TYPE.SEQUENCE, new BT(this.SomethingInmyHand), new BT(this.ReplaceNewFood));

        Seq_CheckBread = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.breadList, MaxNumber)),
                                                    new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Bread)));
                                                    


        Seq_CheckBeef = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.hamburgerList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Hamburger)));

        Seq_CheckTomato = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.tomatoList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Tomato)));


        Seq_CheckSalad = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.saladList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Salad)));
       // Sel_ImdoingSomething = new BT(NODE_TYPE.SELECTOR,  Sel_ToppingGuy, new BT(() => GoTospecificLocation(GameLinks.gl.toppingTableTransform)));

        Sel_ToppingGuy = new BT(NODE_TYPE.SELECTOR, Seq_ImdoingSomething, Seq_CheckBread, Seq_CheckBeef, Seq_CheckTomato, Seq_CheckSalad,new BT(() => GoTospecificLocation(GameLinks.gl.toppingTableTransform)));


    }

    private void Update()
    {
        Sel_ToppingGuy.Evaluate();
    }







    BT_VALUE ReplaceNewFood()
    {
        BT_VALUE b = BT_VALUE.RUNNING;
        GameObject food = GetComponentInChildren<Food>().gameObject;

        Transform t = Helper.GiveTheToppingLocation(food.GetComponent<Food>());
       
        move.navMeshAgent.SetDestination(t.position);
        if (Helper.CheckDistance(this.transform, t, checkDistanceVariation))
        {
            ToppingChecker.HasSomethingOnhisHand = false;
            food.transform.position = t.position;
            food.transform.SetParent(GameLinks.gl.foodParents);
            food.gameObject.AddComponent<Rigidbody>();
            Helper.AddOneFoodObjectOfList(Helper.GiveTypeList(food));

            b = BT_VALUE.SUCCESS;


        }


        return b;
    }
    BT_VALUE SomethingInmyHand()
    {
        BT_VALUE b = BT_VALUE.FAIL;
        if (this.GetComponentInChildren<Food>())
        {
            b = BT_VALUE.SUCCESS;
        }
       return b;
    }

    //seq_checkBread //
    BT_VALUE ListIsMissing(List<Food> list, int maxNumberOfTopping)
    {
        BT_VALUE b = BT_VALUE.FAIL;
        if (!Helper.CheckNumberOfToppings(list, maxNumberOfTopping))
        {
            //ToppingChecker.IsDoingSomething = true;
            b = BT_VALUE.SUCCESS;
        }
        return b;
    }
    BT_VALUE GoToToppingTable(Transform toppingTableLoc, FoodType type)
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!ToppingChecker.HasSomethingOnhisHand)
        {
            move.navMeshAgent.SetDestination(toppingTableLoc.position);
            b = BT_VALUE.RUNNING;
            if (Helper.CheckDistance(this.transform, toppingTableLoc, checkDistanceVariation))
            {
                ToppingChecker.HasSomethingOnhisHand = true;
                GameObject newFood = Instantiate(FoodManager.Instance.foodPrefabDict[type]);
                newFood.transform.position = GameLinks.gl.trayOfToppingChecker.transform.position;

                newFood.transform.SetParent(this.transform);

            }
        }


        return b;

    }
    //BT_VALUE PrepareMissingTopping(FoodType type, float timeToMakeThisType)
    //{
    //    BT_VALUE b = BT_VALUE.RUNNING;

    //    if (true)
    //    {
    //        ToppingChecker.HasSomethingOnhisHand = true;

    //        b = BT_VALUE.SUCCESS;

    //    }
    //    return b;

    //}
    //BT_VALUE ReplaceNewFood(Transform LocToGo, List<Food> list)
    //{
    //    BT_VALUE b = BT_VALUE.RUNNING;
    //    move.navMeshAgent.SetDestination(LocToGo.position);
    //    GameObject foodParent = LocToGo.gameObject;
    //    if (Helper.CheckDistance(this.transform, LocToGo, checkDistanceVariation))
    //    {
    //        Transform child = this.transform.GetComponentInChildren<Food>().transform;
    //        ToppingChecker.HasSomethingOnhisHand = false;
    //        child.transform.position = LocToGo.position;
    //        child.transform.SetParent(GameLinks.gl.foodParents);
    //        child.gameObject.AddComponent<Rigidbody>();
    //        Helper.AddOneFoodObjectOfList(list);

    //        b = BT_VALUE.SUCCESS;


    //    }


    //    return b;
    //}


    BT_VALUE GoTospecificLocation(Transform transform)
    {
        BT_VALUE b = BT_VALUE.RUNNING;

        move.navMeshAgent.SetDestination(transform.position);
        if (Helper.CheckDistance(this.transform,transform, checkDistanceVariation))
        {
            b = BT_VALUE.SUCCESS;
        }

        return b;
    }
}
