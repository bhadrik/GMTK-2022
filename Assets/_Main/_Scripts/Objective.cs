using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    #region  Variable
    //------------------------------------//

    [HideInInspector] public Transform playerStart;
    [SerializeField] public int moves;

    Tile[] tiles;

    public bool Completed{
        get
        {
            bool allDone = true;

            foreach (var tile in tiles)
            {
                allDone = allDone && tile.done;
                if(!allDone) break;
            }

            return allDone;
        }
    }

    [HideInInspector]
    public UnityEvent onObjectiveComplete;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        tiles = GetComponentsInChildren<Tile>();

        foreach (var tile in tiles)
        {
            tile.onFill.AddListener(OnAnyTileFill);
        }

        playerStart = transform.GetChild(0);
    }

    //------------------------------------//
    #endregion



    #region  Private
    //------------------------------------//
    
    private void OnAnyTileFill(){
        if(Completed){
            onObjectiveComplete.Invoke();
        }
    }

    //------------------------------------//
    #endregion
    
}
