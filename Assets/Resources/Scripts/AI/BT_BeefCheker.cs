using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT_lib;
using UnityEngine.UI;

public class BT_BeefCheker : MonoBehaviour
{
    //
    public int order;
    Movement move;
    BeefCheker ovenChecker;
    BT Seq_MaKeBeefOnOven;
    BT Seq_CheckBurningBeef;
    BT Sel_OvenCheckerAI;
    BT Action_GoToInitialPositon;
    BT_MainChef mainchef;
    Button button;
    public float checkDistanceVariation;


    private void Start()
    {

        mainchef = FindObjectOfType<BT_MainChef>();
        mainchef.Order = order;
        move = GetComponent<Movement>();
        ovenChecker = GetComponent<BeefCheker>();
        // Sel_OvenCheckerAI = new BT(NODE_TYPE.SELECTOR, Seq_MaKeBeefOnOven, new BT(this.TakeNewBeef));

        Seq_CheckBurningBeef = new BT(NODE_TYPE.SEQUENCE, new BT(this.IfBurgerIsReady), new BT(this.TurnOffTheOven));
        Seq_MaKeBeefOnOven = new BT(NODE_TYPE.SEQUENCE, new BT(this.CheckIfWeHaveOrder), new BT(this.CheckIfAvailabeStove), new BT(this.TakeNewBeef), new BT(this.PutBeefOnStove));
        Action_GoToInitialPositon = new BT(this.Ac_GoToInitialPositon);


        Sel_OvenCheckerAI = new BT(NODE_TYPE.SELECTOR, Seq_CheckBurningBeef, Seq_MaKeBeefOnOven, Action_GoToInitialPositon);

        button = FindObjectOfType<OrderButton>().GetComponent<Button>();
        button.onClick.AddListener(() => this.AddOneOrder());
    }

    public void AddOneOrder()
    {
        order += 1;
    }
    private void Update()
    {
        Sel_OvenCheckerAI.Evaluate();
    }

    //Seq_MaKeBeefOnOven Actions
    BT_VALUE CheckIfWeHaveOrder()
    {
        BT_VALUE check = BT_VALUE.FAIL;


        //check if we have order

        if (order > 0  )
        {
            GameLinks.gl.trayOfOvenChecker.gameObject.SetActive(true);
            check = BT_VALUE.SUCCESS;
        }

        return check;

    }
    BT_VALUE CheckIfAvailabeStove()
    {
        BT_VALUE check = BT_VALUE.FAIL;

        if (Helper.CheckIfAvailabeStove())
        {

            check = BT_VALUE.SUCCESS;
        }
        return check;
    }

    BT_VALUE TakeNewBeef()
    {
        BT_VALUE b = BT_VALUE.SUCCESS;
        if (!ovenChecker.HasTheBeefOnHisHand)
            {
            b = BT_VALUE.RUNNING;
            move.navMeshAgent.SetDestination(GameLinks.gl.hamburgerLocation.position);
            if (Helper.CheckDistance(this.transform, GameLinks.gl.hamburgerLocation, checkDistanceVariation))
            {
                ovenChecker.HasTheBeefOnHisHand = true;
                //decrese beef number from topping list 
                Helper.PickUpOneFoodObjectOfList(FoodManager.Instance.hamburgerList);
                //make a prefab on top of the head
                GameObject beef = Instantiate(FoodManager.Instance.foodPrefabDict[FoodType.Hamburger]);
                beef.transform.position = GameLinks.gl.trayOfOvenChecker.transform.position;
                beef.transform.SetParent(this.transform);




                //FoodManager.Instance.hamburgerList.Clear();
                //add the beef prefab in tray 
                //decrese the UI number 
            }
        }


        return b;
    }
  //  Stove s;
    BT_VALUE PutBeefOnStove()
    {
        BT_VALUE b = BT_VALUE.RUNNING;

        GameObject stove = Helper.FindAvailableStove();
        move.navMeshAgent.SetDestination(stove.transform.position);
        if (Helper.CheckDistance(this.transform, stove.transform, checkDistanceVariation) && ovenChecker.HasTheBeefOnHisHand && !ovenChecker.TuredOnTheOven)
        {
            Transform child = this.transform.GetComponentInChildren<Food_Beef>().transform;
            order -=1;
            child.position = stove.transform.position;
            child.SetParent(stove.transform);
            stove.GetComponent<Stove>().IsFull = true;
            ovenChecker.HasTheBeefOnHisHand = false;
            stove.GetComponent<Stove>().SwitchStove();
            b = BT_VALUE.SUCCESS;


            //add the beef prefab in tray 
            //decrese beef number from topping list 
            //decrese the UI number 
        }

        return b;
    }
    //Seq_MaKeBeefOnOven actions done

    //seq_CheckBurningBeef Actions

    BT_VALUE IfBurgerIsReady()
    {
        BT_VALUE b = BT_VALUE.FAIL;

        if (Helper.WhichBeefReadyToPicked())
        {
            if (Helper.WhichBeefReadyToPicked().GetComponentInChildren<Stove>().stoveIsOn)
            {
                b = BT_VALUE.SUCCESS;


            }
        }
        return b;

    }
    BT_VALUE TurnOffTheOven()
    {
        BT_VALUE b = BT_VALUE.RUNNING;
        
        GameObject stove = Helper.WhichBeefReadyToPicked();
        move.navMeshAgent.SetDestination(stove.transform.position);
        if (Helper.CheckDistance(this.transform, stove.transform, checkDistanceVariation))
        {
            stove.GetComponent<Stove>().SwitchStove();
            b = BT_VALUE.SUCCESS;
        }
        return b;

    }
    BT_VALUE Ac_GoToInitialPositon()
    {
        BT_VALUE b = BT_VALUE.RUNNING;

        move.navMeshAgent.SetDestination(GameLinks.gl.CustomerTable.position);
        if (Helper.CheckDistance(this.transform, GameLinks.gl.CustomerTable.transform, checkDistanceVariation))
        {
            GameLinks.gl.trayOfOvenChecker.gameObject.SetActive(false);
            RestartTheAI();
            b = BT_VALUE.SUCCESS;

        }

        return b;
    }
    public void RestartTheAI()
    {

    }



}
