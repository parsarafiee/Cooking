using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILinks : MonoBehaviour
{
    #region Singleton

    private static UILinks instance;
    public static UILinks Instance
    {
        get
        {
            return instance ?? (instance = GameObject.FindObjectOfType<UILinks>());
        }
    }

    #endregion

    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject OrderPanel;

    public GameObject GetMenuPanel()
    {
        return MenuPanel;
    }

    public GameObject GetOrderPanel()
    {
        return OrderPanel;
    }
}
