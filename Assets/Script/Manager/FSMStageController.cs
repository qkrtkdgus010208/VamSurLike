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

    public void EnterStage()
    {
        stageFSM = new FSM(new FSMStageStateEnter());
    }

    public void ChangeState(FSMStateBase inFSMState)
    {
        if (stageFSM != null)
        {
            stageFSM.ChangeState(inFSMState);
        }
    }

    public void OnUpdate(float inDeltaTime)
    {
        if (stageFSM != null)
        {
            stageFSM.OnUpdateState(inDeltaTime);
        }
    }

    private FSM stageFSM = null;
}
