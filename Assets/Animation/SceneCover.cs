using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneCover : MonoBehaviour
{
    
    #region  Variable
    //------------------------------------//

    public UnityEvent reachMax;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    

    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//
    
    public void OnRechMax(){
        reachMax?.Invoke();
    }

    //------------------------------------//
    #endregion




    #region  Private
    //------------------------------------//
    
    

    //------------------------------------//
    #endregion
    
}
