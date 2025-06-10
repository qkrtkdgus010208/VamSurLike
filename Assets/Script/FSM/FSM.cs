using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public FSM(FSMStateBase inFSMState)
    {
        CurrentState = inFSMState;

        if (inFSMState != null )
        {
            CurrentState.OnEnter();
        }
    }

    public FSMStateBase CurrentState { get; private set; }

    public void ChangeState(FSMStateBase inFSMState)
    {
        if (inFSMState == null)
        {
            return;
        }

        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = inFSMState;
        if (CurrentState != null)
        {
            CurrentState.OnEnter();
        }
    }

    public void OnUpdateState(float inDeltaTime)
    {
        if(CurrentState != null)
        {
            CurrentState.OnProgress(inDeltaTime);
        }
    }
}
