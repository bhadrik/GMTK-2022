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

    [SerializeField] LineRenderer deshLine;
    [SerializeField] LineRenderer fullLine;

    [Space]
    [SerializeField] Color enableTileColor;
    public bool done;


    [HideInInspector]
    public UnityEvent onFill;

    AudioSource sound;
    MeshRenderer tileRenderer;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        tileRenderer = GetComponent<MeshRenderer>();
        sound = GetComponent<AudioSource>();
    }

    private void Start() {
        fullLine.gameObject.SetActive(done);
        deshLine.gameObject.SetActive(!done);
    }

    private void OnTriggerEnter(Collider other)
    {
        // In game tile touch
        if(done) return;

        else if(other.gameObject.CompareTag(tileType.ToString())){
            done = true;

            deshLine.material.SetInt("_Animated", 0);
            
            tileRenderer.material.SetColor("_Color", enableTileColor);
            
            fullLine.gameObject.SetActive(true);
            deshLine.gameObject.SetActive(false);

            sound.Play();
            onFill.Invoke();
        }
    }

    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//
    

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