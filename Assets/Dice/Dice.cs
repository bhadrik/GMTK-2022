using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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