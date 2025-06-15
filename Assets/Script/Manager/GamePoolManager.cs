using System.Collections.Generic;
using UnityEngine;

public class GamePoolManager
{
    private static GamePoolManager instance;

    public static GamePoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GamePoolManager();
            }

            return instance;
        }
    }

    private Dictionary<SkillType, Queue<SkillBase>> SkillPool;

    public void Init()
    {
        SkillPool = new Dictionary<SkillType, Queue<SkillBase>>();
    }

    public void Clear()
    {
        SkillPool.Clear();
        SkillPool = null;
    }

    public void EnqueueSkillPool(SkillBase skill)
    {
        if (SkillPool == null)
        {
            return;
        }

        if (SkillPool.ContainsKey(skill.SkillType) == false)
        {
            SkillPool.Add(skill.SkillType, new Queue<SkillBase>());
        }

        SkillPool[skill.SkillType].Enqueue(skill);
    }

    public SkillBase DequeueSkillPool(SkillType skillType)
    {
        if (SkillPool == null)
        {
            return null;
        }

        if (SkillPool.ContainsKey(skillType) == false)
        {
            return null;
        }

        return SkillPool[skillType].Dequeue();
    }
}
