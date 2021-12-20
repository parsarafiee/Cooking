using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    public Order order { get; set; }

    [SerializeField] Image timer;

    public void Refresh()
    {
        timer.fillAmount = Mathf.Clamp01(order.timeRemaining / order.TIME_TO_PROCEED_ORDER);
    }
}
