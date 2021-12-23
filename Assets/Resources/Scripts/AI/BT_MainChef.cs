using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;
using UnityEngine.UI;


public class BT_MainChef : MonoBehaviour
{
    Movement move;
    BT seq_BurgerIsReady;
    BT seq_ChechTheBeef;
    BT seq_MakeTheBurger;
    BT sel_MainChef;

    public Text text;
    BT_BeefCheker bt_beefCheker;
    MainChef mainChef;

    float scored = 0;
   // public GameObject hamburgerPrefab;
    public int NumberOfBread;
    public int NumberOfBeef;
    public int NumberOfTomato;
    public int NumberOfSalad;

    public float checkDistanceVariation;

    public int Order { get; set; }
    private void Start()
    {
        bt_beefCheker = FindObjectOfType<BT_BeefCheker>();
        //hamburgerPrefab = Resources.Load<GameObject>("Prefabs/Hamburger");
        GameLinks.gl.HamburgerPrefab.gameObject.SetActive(false);

        move = GetComponent<Movement>();
        mainChef = GetComponent<MainChef>();
     //   seq_BurgerIsReady = new BT(NODE_TYPE.SEQUENCE,new BT(this.CHechIfHeHasTHeBurger));
       seq_MakeTheBurger = new BT(NODE_TYPE.SEQUENCE, new BT(this.GetBread),new BT(this.GetTomato),new BT(this.GetSalad),new BT(this.GetBeef),new BT(this.DropHaburgerInFrontDesk));
        seq_ChechTheBeef = new BT(NODE_TYPE.SEQUENCE, new BT(this.CheckBurgerIsReady), seq_MakeTheBurger);
        sel_MainChef = new BT(NODE_TYPE.SELECTOR, seq_ChechTheBeef, new BT(this.Ac_GoToInitialPositon));
    }
    private void Update()
    {
        text.text = scored.ToString();
           Order = bt_beefCheker.order;
        sel_MainChef.Evaluate();

    }




    //BT_VALUE OrderIsDone()
    //{
    //    BT_VALUE b = BT_VALUE.FAIL;
    //    if (mainChef.ImDoneWithTheORder)
    //    {

    //    }
    //    return b;

    //}

    //BT_VALUE CheckIfWeHaveOrder()
    //{
    //    BT_VALUE check = BT_VALUE.FAIL;


    //    //check if we have order

    //    if (Order>0)
    //    {
    //        check = BT_VALUE.SUCCESS;
    //    }

    //    return check;

    //}
    BT_VALUE CheckBurgerIsReady()
    {
        BT_VALUE b = BT_VALUE.FAIL;
        if (Helper.WhichBeefReadyToPickedForChef()|| mainChef.hasTHeHamBurger)
        {
            GameLinks.gl.trayOfMainChef.gameObject.SetActive(true);

            b = BT_VALUE.SUCCESS;
           
        }
        return b;
    }
    BT_VALUE GetBread()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!mainChef.HasBread)
        {

            b = BT_VALUE.RUNNING;
            move.navMeshAgent.SetDestination(GameLinks.gl.breadLocation.position);
            if (Helper.CheckDistance(this.transform, GameLinks.gl.breadLocation, checkDistanceVariation))
            {
                mainChef.HasBread = true;
                Helper.PickUpFoodsOfList(FoodManager.Instance.breadList,NumberOfBread);
                GameLinks.gl.HamburgerPrefab.gameObject.SetActive(true);
            }
        }
        return b;

    }
    BT_VALUE GetTomato()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!mainChef.HasTomato)
        {
            b = BT_VALUE.RUNNING;
            move.navMeshAgent.SetDestination(GameLinks.gl.tomatoLocation.position);
            if (Helper.CheckDistance(this.transform, GameLinks.gl.tomatoLocation, checkDistanceVariation))
            {
                mainChef.HasTomato = true;
                Helper.PickUpFoodsOfList(FoodManager.Instance.tomatoList, NumberOfTomato);
 
            }
        }
        return b;

    }
    BT_VALUE GetSalad ()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!mainChef.HasSalad)
        {
            b = BT_VALUE.RUNNING;
            move.navMeshAgent.SetDestination(GameLinks.gl.sladLocation.position);
            if (Helper.CheckDistance(this.transform, GameLinks.gl.sladLocation, checkDistanceVariation))
            {
                mainChef.HasSalad = true;
                Helper.PickUpFoodsOfList(FoodManager.Instance.saladList, NumberOfSalad);
            }
        }
        return b;

    }  
    BT_VALUE GetBeef()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!mainChef.HasBeef)
        {
             b = BT_VALUE.RUNNING;

            GameObject stove = Helper.WhichBeefReadyToPickedForChef();
            move.navMeshAgent.SetDestination(stove.transform.position);
            if (Helper.CheckDistance(this.transform, stove.transform, checkDistanceVariation))
            {
                mainChef.HasBeef = true;
                if (stove.GetComponentInChildren<Food_Beef>())
                {
                    GameObject.Destroy(stove.GetComponentInChildren<Food_Beef>().gameObject);
                }
                mainChef.hasTHeHamBurger = true;
                stove.GetComponent<Stove>().IsFull = false;
                stove.GetComponent<Stove>().BurgerIsReady = false;
            }

        }
       
        return b;


    }

    BT_VALUE DropHaburgerInFrontDesk()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;

        if (mainChef.hasTHeHamBurger)
        {

            b = BT_VALUE.RUNNING;
            move.navMeshAgent.SetDestination(GameLinks.gl.dropBirgerTable.position);
            if (Helper.CheckDistance(this.transform, GameLinks.gl.dropBirgerTable, checkDistanceVariation))
            {
                GameLinks.gl.HamburgerPrefab.gameObject.SetActive(false);
                GameObject haburger = Instantiate(GameLinks.gl.HamburgerPrefab, GameLinks.gl.HaburgerFinishLocation.position, Quaternion.identity);
                haburger.AddComponent<Rigidbody>();
                haburger.SetActive(true);
                mainChef.ResetChef();
                Order -= 1;
                mainChef.ImDoneWithTheORder = true;
                scored += 10;
                GameObject.Destroy(UIManager.Instance.orders[0].gameObject);
                UIManager.Instance.orders.Remove(UIManager.Instance.orders[0]);
                // Delet Order add some finishing music 

            }
        }

            return b;

    }

    BT_VALUE Ac_GoToInitialPositon()
    {
        BT_VALUE b = BT_VALUE.RUNNING;
        move.navMeshAgent.SetDestination(GameLinks.gl.MainChefLocation.position);
        if (Helper.CheckDistance(this.transform, GameLinks.gl.MainChefLocation.transform, checkDistanceVariation))
        {
            GameLinks.gl.trayOfMainChef.gameObject.SetActive(false);
          //  RestartTheAI();
            b = BT_VALUE.SUCCESS;

        }

        return b;
    }

}
