using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    #region  Variable
    //------------------------------------//
    #if UNITY_EDITOR
    int moveCountsAfterClear;
    #endif

    [Header("General")]
    [SerializeField] Dice player;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] GameObject finishCanvas;
    [SerializeField] GameObject uiCanvas;
    [SerializeField] SceneCover blackCurtain;
    [SerializeField] TextMeshProUGUI levelCountText;


    [Header("Clip")]
    [SerializeField] AudioClip positiveClip;
    [SerializeField] AudioClip negativeClip;


    [Header("Level")]
    [SerializeField] GameObject[] levels;

    
    Objective lodedLevel;
    AudioSource audioSource;
    AnimateUI endUI;

    int loadedLevelIndex;
    int remainMoves;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        player.onPlayerMove.AddListener(OnPlayerMove);
        blackCurtain.reachMax.AddListener(ActualLevelChange);
        audioSource = GetComponent<AudioSource>();
        endUI = finishCanvas.GetComponentInChildren<AnimateUI>();
    }

    private void Start() {
        LoadLevel(0, "Level 1");
        moveText.text = "";
        levelCountText.text = $"{loadedLevelIndex+1}/{levels.Length}";
    }

// #if UNITY_EDITOR
    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)){
            PreviousLevel();
        }
        else if (Input.GetKeyDown(KeyCode.L)){
            NextLevel();
        }
    }
// #endif
    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//

    public void RestartGame(){
        SceneManager.LoadSceneAsync(0);
    }

    public void RestartLevel(string msg){
        player.enabled = false;

        if(string.IsNullOrEmpty(msg))
            msg = $"Level {loadedLevelIndex+1}";
            
        LoadLevel(loadedLevelIndex, msg);
    }

    public void OpenLink(){
        Application.OpenURL("https://pixabay.com/sound-effects/");
    }

// #if UNITY_EDITOR
    public void NextLevel(){
        if(loadedLevelIndex == 9) return;
        LoadLevel(loadedLevelIndex+1, "Next Level " + (loadedLevelIndex + 2));
    }

    public void PreviousLevel(){
        if(loadedLevelIndex == 0) return;
        LoadLevel(loadedLevelIndex-1, "Previous Level " + (loadedLevelIndex));
    }
// #endif
    //------------------------------------//
    #endregion




    #region  Private
    //------------------------------------//
    
    private void LoadLevel(int i, string msg){
        // Debug.Log("Load level: "+ i + " Level.Length" + levels.Length);

        player.enabled = false;

        loadedLevelIndex = i;

        if(i == levels.Length){
            //Show congrets on all level complete
            ShowFinishUI();
        }
        else{
            blackCurtain.StartCover(msg);
        }
    }

    //Called by animator event after scene is covered properly
    private void ActualLevelChange(){
        if(lodedLevel != null){
            Destroy(lodedLevel.gameObject);
        }

        lodedLevel = Instantiate(levels[loadedLevelIndex], Vector3.zero, Quaternion.identity).GetComponent<Objective>();
        
        lodedLevel.onObjectiveComplete.AddListener(() => StartCoroutine(OnLevelComplete()));

        player.enabled = true;
        player.ResetMovingConstrains();
        player.transform.SetPositionAndRotation(lodedLevel.playerStart.position, lodedLevel.playerStart.rotation);

        remainMoves = lodedLevel.moves;
        moveText.text = remainMoves.ToString();
        levelCountText.text = $"{loadedLevelIndex+1}/{levels.Length}";
    }

    private void ShowFinishUI(){
        finishCanvas.SetActive(true);
        uiCanvas.SetActive(false);
        endUI.ShowPanel();
    }

    private void OnPlayerMove(){
        remainMoves--;
        moveText.text = remainMoves.ToString();

        if(remainMoves == 0){
            audioSource.PlayOneShot(negativeClip);
            RestartLevel("Out of moves");
        }
    }

    private IEnumerator OnLevelComplete(){
        // Debug.Log("Level complete");
        player.enabled = false;

        audioSource.PlayOneShot(positiveClip);

        yield return new WaitForSeconds(1.0f);

        LoadLevel(loadedLevelIndex+1, $"Level {loadedLevelIndex+2}");
    }

    //------------------------------------//
    #endregion
    
}