using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitieManager 
{

    #region Singleton
    public static AbilitieManager Instance
    {
        get
        {
            if (instance == null)
                instance = new AbilitieManager();
            return instance;
        }
    }

    private static AbilitieManager instance;

    private AbilitieManager() { }
    #endregion

   
}
