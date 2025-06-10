using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageStateProgress : FSMStateBase
{
    public FSMStageStateProgress() : base(EFSMStageStateType.StageProgress)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Stage State Progress Enter");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnProgress(float inDeltaTime)
    {
        base.OnProgress(inDeltaTime);
    }
}
