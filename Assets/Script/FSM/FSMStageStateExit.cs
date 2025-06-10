﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageStateExit : FSMStateBase
{
    public FSMStageStateExit() : base(EFSMStageStateType.StageEnd)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
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
