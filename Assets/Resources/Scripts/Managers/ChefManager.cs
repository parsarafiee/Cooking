using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefManager
{
    #region Singleton
    public static ChefManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ChefManager();
            return instance;
        }
    }

    private static ChefManager instance;



    private ChefManager() { }
    #endregion
    internal void Initialize()
    {
    }

    internal void Refresh()
    {
    }
}
