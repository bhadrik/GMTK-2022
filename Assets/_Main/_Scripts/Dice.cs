using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    #region  Variable
    //------------------------------------//

    [SerializeField] int speed = 300;
    bool isMoving = false;

    [SerializeField] bool blockLeft;
    [SerializeField] bool blockRight;
    [SerializeField] bool blockForward;
    [SerializeField] bool blockBack;

    [HideInInspector]
    public UnityEvent onPlayerMove;

    AudioSource sound;

    //------------------------------------//
    #endregion




    #region  Unity Method
    //------------------------------------//

    private void Awake() {
        sound = GetComponent<AudioSource>();
    }

    void Update() {

        if(Input.GetKey(KeyCode.U)){
            
        }

        if (isMoving) {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !blockRight) {
            StartCoroutine(Roll(Vector3.right));
        } else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !blockLeft) {
            StartCoroutine(Roll(Vector3.left));
        } else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !blockForward) {
            StartCoroutine(Roll(Vector3.forward));
        } else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !blockBack) {
            StartCoroutine(Roll(Vector3.back));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("block left"))
            blockLeft = true;
        else if(other.gameObject.CompareTag("block right"))
            blockRight = true;
        else if(other.gameObject.CompareTag("block forward"))
            blockForward = true;
        else if(other.gameObject.CompareTag("block back"))
            blockBack = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("block left"))
            blockLeft = false;
        else if(other.gameObject.CompareTag("block right"))
            blockRight = false;
        else if(other.gameObject.CompareTag("block forward"))
            blockForward = false;
        else if(other.gameObject.CompareTag("block back"))
            blockBack = false;
    }

    //------------------------------------//
    #endregion




    #region  Public
    //------------------------------------//
    
    public void ResetMovingConstrains(){
        blockBack = false;
        blockForward = false;
        blockLeft = false;
        blockRight = false;
    }

    public void GoingAnim(){
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(Vector3.one * 1.05f, 0.1f));
        s.Append(transform.DOScale(Vector3.zero, 0.4f));
    }

    public void CommingAnim(){
        Sequence s = DOTween.Sequence();
        // s.Append(transform.DOScale(Vector3.one, 0.5f));
        s.Append(transform.DOScale(Vector3.one * 1.05f, 0.4f));
        s.Append(transform.DOScale(Vector3.one, 0.1f));
    }

    //------------------------------------//
    #endregion




    #region  Private
    //------------------------------------//
    
    IEnumerator Roll(Vector3 direction) {
        isMoving = true;

        onPlayerMove.Invoke();

        sound.Play();

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0) {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;
    }

    //------------------------------------//
    #endregion
    
}