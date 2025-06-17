using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManualMissile : SkillBase
{
    void Start()
    {

    }

    public override void FireSkill(ActiveSkillData activeSkillData, Vector3 startPos, Vector3 startDir)
    {
        base.FireSkill(activeSkillData, startPos, startDir);

        SkillType = SkillType.ManualMissile;

        StartCoroutine(OnMissileLiftTime());
    }

    public IEnumerator OnMissileLiftTime()
    {
        float currentLiftTime = 0.0f;

        while (true)
        {
            Vector3 addForceVector = StartDir * ActiveSkillData.Speed * Time.deltaTime;
            transform.position += new Vector3(addForceVector.x, 0, addForceVector.z);
            currentLiftTime += Time.deltaTime;

            if (currentLiftTime > 2.0f)
            {
                break;
            }
            yield return null;
        }

        StopSkill();
    }
}
