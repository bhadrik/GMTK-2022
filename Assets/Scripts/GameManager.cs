using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    #region  Variable
    //------------------------------------//
    [Header("General")]
    [SerializeField] Dice player;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] GameObject finishGamePanel;
    [SerializeField] SceneCover sceneCover;


    [Header("Level")]
    [SerializeField] GameObject[] levels;

    [Space]
    [SerializeField] Material[] tileMaterialInSeries;
    
    Objective lodedLevel;
    Animator levelChangeAnimation;
    int loadIndex;

    int remainMoves;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        player.onPlayerMove.AddListener(OnPlayerMove);
        sceneCover.reachMax.AddListener(ActualLevelChange);
        levelChangeAnimation = sceneCover.gameObject.GetComponent<Animator>();
    }

    private void Start() {
        LoadLevel(0);
        moveText.text = "";
    }

    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//
    
    public Material GetMaterialFromType(TileNumber t){
        return tileMaterialInSeries[(int)t];
    }

    public void GameOver(){
        Debug.Log("Game Over bro");
    }

    //------------------------------------//
    #endregion




    #region  Private
    //------------------------------------//
    
    private void LoadLevel(int i){

        loadIndex = i;
        if(i == levels.Length){
            //Show congrets all level done Unity
            ShowFinishUI();
        }
        else{
            // This animation has event to load new level
            levelChangeAnimation.Play("cover in");
        }
    }

    //Called by animator event after scene is covered properly
    private void ActualLevelChange(){
        if(lodedLevel != null){
            Destroy(lodedLevel.gameObject);
        }

        lodedLevel = Instantiate(levels[loadIndex], Vector3.zero, Quaternion.identity).GetComponent<Objective>();
        
        lodedLevel.onObjectiveComplete.AddListener(OnLevelComplete);

        player.transform.SetPositionAndRotation(lodedLevel.playerStart.position, lodedLevel.playerStart.rotation);

        remainMoves = lodedLevel.moves;
        moveText.text = remainMoves.ToString();
    }

    private void ShowFinishUI(){
        finishGamePanel.SetActive(true);
    }

    private void OnPlayerMove(){
        remainMoves--;
        moveText.text = remainMoves.ToString();
    }

    private void OnLevelComplete(){
        LoadLevel(loadIndex+1);
    }

    //------------------------------------//
    #endregion
    
}