using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    #region Singleton
    private static UIManager instance;
    private UIManager() { }

    public static UIManager Instance { get { return instance ?? (instance = new UIManager()); } }
    #endregion

    private UILinks ui; //Shortcut to UILinks.Instance
    
    public void Initialize()
    {
        if (instance != null) return;
        instance = new UIManager();
        LoadMenu();
    }

    private void LoadMenu()
    {

    }

    private HashSet<Sprite> LoadMenuIcons()
    {
        HashSet<Sprite> Icons = new HashSet<Sprite>();



        return Icons;
    }
}
