using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    #region Singleton
    private static UIManager instance;
    private UIManager() { }

    public static UIManager Instance { get { return instance ?? (instance = new UIManager()); } }
    #endregion

    private Object orderPrefab;

     public List<GameObject> orders;
     public Stack<GameObject> toRemove;
     public Stack<GameObject> toAdd;

    public void Initialize()
    {
        LoadRestaurantMenu();
        orderPrefab = Resources.Load("Prefabs/UIOrders/BurgerOrder", typeof(GameObject)) as GameObject;
        orders = new List<GameObject>();
        toRemove = new Stack<GameObject>();
        toAdd = new Stack<GameObject>();
    }

    private void LoadRestaurantMenu()
    {
        Object[] buttonPrefabs = Resources.LoadAll("Prefabs/UIMenuButton", typeof(GameObject));

        foreach (var button in buttonPrefabs)
        {
            GameObject.Instantiate(button, UILinks.Instance.GetMenuPanel().transform);
        }
    }

    public void CreateNewOrderUI(Order order)
    {
        GameObject orderUi = GameObject.Instantiate(orderPrefab, UILinks.Instance.GetOrderPanel().transform) as GameObject;  
        toAdd.Push(orderUi);
        orderUi.GetComponent<OrderUI>().order = order;
    }

    public void Update()
    {
        foreach (var order in orders)
        {
            OrderUI uiOrder = order.GetComponent<OrderUI>();
            if (uiOrder.order.isActive)
                uiOrder.Refresh();
            else
            {
                toRemove.Push(order);
            }
        }

        while (toRemove.Count > 0)
        {
            var order = toRemove.Pop();
            orders.Remove(order);
          //  GameObject.Destroy(order);
        }

        while (toAdd.Count > 0)
            orders.Add(toAdd.Pop());
    }

    public void destroyObj()
    {

    }
}

