using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageStateEnter : FSMStateBase
{
    public FSMStageStateEnter() : base(EFSMStageStateType.StageStart)
    {

    }

    private float countDown = 3;
    private float durationTime = 0;

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Stage State Enter, Call Exit");
        durationTime = 0;
    }

    public override void OnProgress(float inDeltaTime)
    {
        base.OnProgress(inDeltaTime);
        durationTime += inDeltaTime;
        if (durationTime > 1.0f)
        {
            if (countDown <= 0)
            {
                FSMStageController.Instance.ChangeState(new FSMStageStateProgress());
            }
            else
            {
                countDown--;
                Debug.Log("Count Down - " + countDown);
            }
            durationTime = 0f;
        }
    }
}
