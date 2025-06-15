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

    public delegate void OnMove(Vector3 direct);
    public delegate void OnMoveStart();
    public delegate void OnMoveEnd();

    public OnMove OnMoving { get; set; }
    public OnMoveStart OnMoveStarting { get; set; }
    public OnMoveEnd OnMoveEnding { get; set; }

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
    }

    public void Clear()
    {

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
            if (OnMoving != null)
            {
                OnMoving(moveVectorNormal);
            }
            if (isMove == false)
            {
                if (OnMoveStarting != null)
                {
                    OnMoveStarting();
                }

                isMove = true;
            }
        }
        else
        {
            if (isMove == true)
            {
                if (OnMoving != null)
                {
                    OnMoveEnding();
                }

                isMove = false;
            }
        }
    }
}
