using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType SkillType { get; set; }

    public virtual void FireSkill(Vector3 startPos, Vector3 startDir)
    {

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
