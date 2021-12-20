using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager
{
    #region Singleton
    private static OrderManager instance;
    private OrderManager() { }

    public static OrderManager Instance { get { return instance ?? (instance = new OrderManager()); } }
    #endregion

    public HashSet<Order> orders;
    private Stack<Order> toRemove;
    private Stack<Order> toAdd;

    public readonly int MAX_NUMBER_ORDERS = 8;
    public int OrderCount => orders.Count;

    public void Initialize()
    {
        orders = new HashSet<Order>();
        toRemove = new Stack<Order>();
        toAdd = new Stack<Order>();
    }

    public void Update()
    {
        foreach (var order in orders)
            if (order.isActive)
                order.Refresh();


        while (toRemove.Count > 0)
        {

            var order = toRemove.Pop();
            orders.Remove(order);
        }

        while (toAdd.Count > 0)
            orders.Add(toAdd.Pop());
    }

    public void OrderDie(Order order)
    {
       
        toRemove.Push(order);
        
    }

    public void AddOrder()
    {
        Order order = new Order();
        UIManager.Instance.CreateNewOrderUI(order);
        toAdd.Push(order);
    }
}
