using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum OrderStatus
{
    SUCCES,
    FAIL,
    RUNNING
}
public class Order
{
    private Timer timer;
    public bool isActive => timer.IsOver == false;
    OrderStatus status;
    public readonly float TIME_TO_PROCEED_ORDER = 60;
    public float timeRemaining => timer.RemainingTime;


    public Order()
    {
        Init();
    }

    private void Init()
    {
        timer = new Timer(TIME_TO_PROCEED_ORDER);
        status = OrderStatus.RUNNING;
        timer.StartCount();
    }

    public void Refresh()
    {
        if (isActive && status == OrderStatus.RUNNING)
        {
            timer.Update();  
        }
    }

}
