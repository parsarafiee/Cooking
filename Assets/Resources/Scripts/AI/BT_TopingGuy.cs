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

        Seq_CheckBread = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.breadList, MaxNumber)),
                                                    new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Bread)),
                                                    new BT(() => ReplaceNewFood(GameLinks.gl.breadLocationSpawner, FoodManager.Instance.breadList)));


        Seq_CheckBeef = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.hamburgerList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Hamburger)),
                                                   new BT(() => ReplaceNewFood(GameLinks.gl.hamburgerLocationSpawner, FoodManager.Instance.hamburgerList)));

        Seq_CheckTomato = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.tomatoList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Tomato)),
                                                   new BT(() => ReplaceNewFood(GameLinks.gl.tomatoLocationSpawner, FoodManager.Instance.tomatoList)));


        Seq_CheckSalad = new BT(NODE_TYPE.SEQUENCE, new BT(() => ListIsMissing(FoodManager.Instance.saladList, MaxNumber)),
                                                   new BT(() => GoToToppingTable(GameLinks.gl.toppingTableTransform, FoodType.Salad)),
                                                   new BT(() => ReplaceNewFood(GameLinks.gl.sladLocationSpawner, FoodManager.Instance.saladList)));


        Sel_ToppingGuy = new BT(NODE_TYPE.SELECTOR, Seq_CheckBread, Seq_CheckBeef, Seq_CheckTomato, Seq_CheckSalad, new BT(() => GoTospecificLocation(GameLinks.gl.toppingTableTransform)));


    }

    private void Update()
    {
        Sel_ToppingGuy.Evaluate();
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
                GameObject newBread = Instantiate(FoodManager.Instance.foodPrefabDict[type]);
                newBread.transform.position = GameLinks.gl.trayOfToppingChecker.transform.position;

                newBread.transform.SetParent(this.transform);

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
    BT_VALUE ReplaceNewFood(Transform LocToGo, List<Food> list)
    {
        BT_VALUE b = BT_VALUE.RUNNING;
        move.navMeshAgent.SetDestination(LocToGo.position);
        GameObject foodParent = LocToGo.gameObject;
        if (Helper.CheckDistance(this.transform, LocToGo, checkDistanceVariation))
        {
            Transform child = this.transform.GetComponentInChildren<Food>().transform;
            ToppingChecker.HasSomethingOnhisHand = false;
            child.transform.position = LocToGo.position;
            child.transform.SetParent(foodParent.transform);
            child.gameObject.AddComponent<Rigidbody>();
            Helper.AddOneFoodObjectOfList(list);

            b = BT_VALUE.SUCCESS;


        }


        return b;
    }


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
