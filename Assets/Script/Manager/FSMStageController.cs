using UnityEngine;

public class FSMStageController
{
    private static FSMStageController instance;

    public static FSMStageController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FSMStageController();
            }

            return instance;
        }
    }

    public void Init()
    {

    }

    public void Clear()
    {

    }
}
