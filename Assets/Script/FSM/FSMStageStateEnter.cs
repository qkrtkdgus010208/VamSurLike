using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStageStateEnter : FSMStateBase
{
    private float countDown = 3;
    private float durationTime = 0;

    public FSMStageStateEnter() : base(EFSMStageStateType.StageStart)
    {

    }
   
    public override void OnEnter()
    {
        base.OnEnter();
        countDown = 0;
        durationTime = 0;

        int currentStageId = GameDataManager.Instance.Stage;
        StageData currentStageData = GameDataManager.Instance.FindStageData(currentStageId);

        if (currentStageData != null)
        {
            foreach (StageUnitData eachNpc in currentStageData.Units)
            {
                // SpawnManager.Instance 강의영상누락
            }
        }
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
