using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType SkillType { get; set; }
    public ActiveSkillData ActiveSkillData { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 StartDir { get; private set; }

    public virtual void FireSkill(ActiveSkillData skillData, Vector3 startPos, Vector3 startDir)
    {
        ActiveSkillData = skillData;
        StartPos = startPos;
        StartDir = startDir;

        transform.position = StartPos;
    }

    public virtual void StopSkill()
    {
        gameObject.SetActive(false);

        GamePoolManager.Instance.EnqueueSkillPool(this);
    }

    public virtual void Update()
    {
        
    }

    public virtual void OnDestory()
    {

    }
}
