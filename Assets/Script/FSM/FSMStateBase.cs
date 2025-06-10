using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateBase
{
    public EFSMStageStateType CurrentStateType {  get; protected set; }
    public FSMStateBase(EFSMStageStateType inType)
    {
        CurrentStateType = inType;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual void OnProgress(float inDeltaTime)
    {

    }
}
