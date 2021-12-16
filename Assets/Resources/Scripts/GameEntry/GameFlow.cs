using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameLinks.gl = GameObject.FindObjectOfType<GameLinks>();
        FoodManager.Instance.Initialize();
        StoveManager.Instance.Initialize();
      //  ChefManager.Instance.Initialize();

        //   Debug.Log(PointsManager.Instance.points.Count);

    }

    private void Update()
    {
        FoodManager.Instance.Refresh();
        StoveManager.Instance.Refresh();
    }


}
