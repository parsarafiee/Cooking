using UnityEngine;

public class OrderButton : MonoBehaviour
{
    public void Order()
    {
        if (OrderManager.Instance.OrderCount >= OrderManager.Instance.MAX_NUMBER_ORDERS) return;
        OrderManager.Instance.AddOrder();

    }
}
