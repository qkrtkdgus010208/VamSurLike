using System;
using UnityEngine;

public class GameControl
{
    private static GameControl instance;

    public static GameControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameControl();
            }

            return instance;
        }
    }

    public Action<Vector3> OnMove { get; set; }
    public Action OnMoveStart { get; set; }
    public Action OnMoveEnd { get; set; }
    public Action<int, Vector3> OnMouseInput { get; set; }

    private GameObject controlObject;
    private UnitMovementBase movementBase;
    private bool isMove;

    public void Init()
    {

    }

    public void SetControlObject(GameObject gameObject)
    {
        controlObject = gameObject;
        movementBase = gameObject.GetComponent<UnitMovementBase>();
    }

    public GameObject GetControlObject()
    {
        return controlObject;
    }

    public void OnUpdate()
    {
        UpdateKeyboard();
        UpdateMouseInput();
    }

    public void Clear()
    {

    }

    private void UpdateMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (OnMouseInput != null)
            {
                OnMouseInput(0, Input.mousePosition);
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (OnMouseInput != null)
            {
                OnMouseInput(1, Input.mousePosition);
            }
        }
    }

    private void UpdateKeyboard()
    {
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += new Vector3(1, 0, 0);
        }

        Vector3 moveVectorNormal = moveVector.normalized;

        if (moveVectorNormal != Vector3.zero)
        {
            if (OnMove != null)
            {
                OnMove(moveVectorNormal);
            }
            if (isMove == false)
            {
                if (OnMoveStart != null)
                {
                    OnMoveStart();
                }

                isMove = true;
            }
        }
        else
        {
            if (isMove == true)
            {
                if (OnMove != null)
                {
                    OnMoveEnd();
                }

                isMove = false;
            }
        }
    }
}
