using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    #region  Variable
    //------------------------------------//

    [SerializeField] public TileNumber tileType;

    [SerializeField] MeshRenderer tileRenderer;
    Animator tileAnimator;
    public bool done;

    [HideInInspector]
    public UnityEvent onFill;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        tileAnimator = GetComponentInChildren<Animator>();
        tileRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // In game tile touch
        if(done) return;

        else if(other.gameObject.CompareTag(tileType.ToString())){
            tileAnimator.Play("tile done");
        }
    }

    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//
    //New tile from SO
    public void GenerateTile(TileNumber type)
    {
        if(type == TileNumber.none){
            type = (TileNumber)UnityEngine.Random.Range(1, 7);
        }

        tileRenderer.material = GameManager.instance.GetMaterialFromType(type);

        if(type != TileNumber.none) DebugActivate();
    }

    public void DebugActivate(){
        tileAnimator.Play("tile done");
    }

    //------------------------------------//
    #endregion




    #region  Private
    //------------------------------------//
    
    

    //------------------------------------//
    #endregion
    
}

public enum TileNumber{
    none,
    one,
    two,
    three,
    four,
    five,
    six,
    deth
}