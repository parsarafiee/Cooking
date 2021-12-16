using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveManager 
{
    #region Singleton
    public static StoveManager Instance
    {
        get
        {
            if (instance == null)
                instance = new StoveManager();
            return instance;
        }
    }
    private static StoveManager instance;
    private StoveManager() { }

    //  private StoveManager() { }
    #endregion

    List<Stove> stoves;

    public void Initialize()
    {
        stoves = new List<Stove>();
        GetAllTheStove();

    }
    void GetAllTheStove()
    {
        for (int i = 0; i < GameLinks.gl.allStovesLocations.Count; i++)
        {
            stoves.Add(GameLinks.gl.allStovesLocations[i].GetComponent<Stove>());
        }
    }
    public void Refresh()
    {
        foreach (Stove stve in stoves)
        {
            stve.Refresh();
        }

    }
}
