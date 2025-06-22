using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public float currentCooltime = 0.0f;

    Dictionary<SkillType, int> levelOfSkill;
    Dictionary<SkillType, ActiveSkillData> currentActiveSkillDatas;
    List<ActiveSkillData> currentManualSkillDatas;

    void Awake()
    {
        levelOfSkill = new Dictionary<SkillType, int>();
        currentActiveSkillDatas = new Dictionary<SkillType, ActiveSkillData>();
        currentManualSkillDatas = new List<ActiveSkillData>();

        GameControl.Instance.OnMouseInput += OnMouseInput;
    }

    void OnDestroy()
    {
        levelOfSkill.Clear();
        levelOfSkill = null;

        currentActiveSkillDatas.Clear();
        currentActiveSkillDatas = null;

        currentManualSkillDatas.Clear();
        currentManualSkillDatas = null;

        GameControl.Instance.OnMouseInput -= OnMouseInput;
    }

    void Update()
    {
        if (FSMStageController.Instance.IsPlayGame() == false)
        {
            return;
        }

        foreach (var eachActiveSkill in currentActiveSkillDatas)
        {
            eachActiveSkill.Value.CurrentCooltime += Time.deltaTime;

            if (eachActiveSkill.Value.CurrentCooltime >= eachActiveSkill.Value.ActiveSkillLevelData.Cooltime)
            {
                if (eachActiveSkill.Value.ActiveType == SkillActiveType.Auto)
                {
                    FireSkill(eachActiveSkill.Value);
                }
            }
        }

        currentCooltime += Time.deltaTime;
    }

    private void OnMouseInput(int index, Vector3 mousePos)
    {
        if (FSMStageController.Instance.IsPlayGame() == false)
        {
            return;
        }

        if (currentManualSkillDatas.Count - 1 < index)
        {
            return;
        }

        if (currentManualSkillDatas[index].CurrentCooltime < currentManualSkillDatas[index].ActiveSkillLevelData.Cooltime)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layermask))
        {
            currentManualSkillDatas[index].FirePosition = hit.point;
            FireSkill(currentManualSkillDatas[index]);
        }
    }

    public void AddSkillData(SkillType skillType)
    {
        SkillData skillData = GameDataManager.Instance.FindSkillData(skillType);

        if (skillData == null)
        {
            return;    
        }

        if (levelOfSkill.ContainsKey(skillType))
        {
            levelOfSkill[skillType]++;
        }
        else
        {
            levelOfSkill.Add(skillType, 1);
        }

        int currentSkillLevel = levelOfSkill[skillType];
        SkillLevelData currentSKillLevelData = GameDataManager.Instance.FindSkillLevelData(skillType, currentSkillLevel);

        if (currentSKillLevelData == null)
        {
            return;
        }

        if (currentActiveSkillDatas.ContainsKey(skillType) == false)
        {
            ActiveSkillData newSkillData = new ActiveSkillData();
            newSkillData.Type = skillType;
            newSkillData.ActiveType = skillData.ActiveType;
            newSkillData.CurrentCooltime = 0.0f;
            newSkillData.ActiveSkillLevelData = currentSKillLevelData;

            currentActiveSkillDatas.Add(skillType , newSkillData);
        }
        else
        {
            currentActiveSkillDatas[skillType].CurrentCooltime = 0.0f;
            currentActiveSkillDatas[skillType].ActiveSkillLevelData = currentSKillLevelData;
        }

        switch (skillData.ActiveType)
        {
            case SkillActiveType.Manual:
                {
                    int findIndex = -1;
                    int currentIndex = 0;
                    foreach (var eachSkill in currentManualSkillDatas)
                    {
                        if (eachSkill.Type == skillType)
                        {
                            findIndex = currentIndex;
                        }
                        currentIndex++;
                    }

                    if (findIndex >= 0)
                    {
                        currentManualSkillDatas[findIndex] = currentActiveSkillDatas[skillType];
                    }
                    else
                    {
                        currentManualSkillDatas.Add(currentActiveSkillDatas[skillType]);
                    }
                }
                break;
        }
    }

    public ActiveSkillData GetCurrentSkillData(SkillType skillType)
    {
        if (currentActiveSkillDatas.ContainsKey(skillType) == false)
        {
            return null;
        }

        return currentActiveSkillDatas[skillType];
    }

    public void FireSkill(ActiveSkillData skillData)
    {
        switch (skillData.Type)
        {
            case SkillType.Missile:
                {
                    for (int fireAngle = 0; fireAngle < 360; fireAngle += 10)
                    {
                        Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);
                        Vector3 shotDirection = new Vector3(Mathf.Cos(fireAngle * Mathf.Deg2Rad), 1, Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                        FireSkillObject(skillData, startPos, shotDirection);
                    }
                }
                break;

            case SkillType.ManualMissile:
                {
                    Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);
                    Vector3 shotDirection = (skillData.FirePosition - transform.position).normalized;

                    FireSkillObject(skillData, startPos, shotDirection);
                }
                break;
        }

        skillData.CurrentCooltime = 0;

        currentCooltime = 0.0f;
    }

    public void FireSkillObject(ActiveSkillData skillData, Vector3 startPos, Vector3 skillDir)
    {
        SkillBase skillObject = GamePoolManager.Instance.DequeueSkillPool(skillData.Type);

        if (skillObject == null)
        {
            SkillBase newSkillObjectPrefab = GameDataManager.Instance.GetSkillObjectPrefab(skillData.Type, skillData.ActiveSkillLevelData.Level);
            skillObject = Instantiate(newSkillObjectPrefab, GameDataManager.Instance.GetSkillRootTransform());

            if (skillObject == null)
            {
                return;
            }
        }

        skillObject.gameObject.SetActive(true);
        skillObject.FireSkill(skillData, startPos, skillDir);
    }
}
