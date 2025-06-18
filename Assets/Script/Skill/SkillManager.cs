using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public float currentCooltime = 0.0f;

    void Awake()
    {
        GameControl.Instance.OnMouseInput += OnMouseInput;
    }

    void OnDestroy()
    {
        GameControl.Instance.OnMouseInput -= OnMouseInput;
    }

    void Update()
    {
        currentCooltime += Time.deltaTime;
    }

    private void OnMouseInput(int index, Vector3 mousePos)
    {
        //if (FSMStageController.Instance.IsPlayGame() == false)
        //{
        //    return;
        //}

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        int layermask = 1 << LayerMask.NameToLayer("Terrain");

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layermask))
        {
            ActiveSkillData newSkillData = new ActiveSkillData();
            newSkillData.Type = SkillType.ManualMissile;
            newSkillData.FirePosition = hit.point;
            newSkillData.Cooltime = 0.5f;
            newSkillData.Speed = 10.0f;
            newSkillData.ActiveLevel = 2;
            FireSkill(newSkillData);
        }
    }

    public void FireSkill(ActiveSkillData skillData)
    {
        if (currentCooltime < skillData.Cooltime)
        {
            return;
        }

        MyPcUnitMovement movement = GetComponent<MyPcUnitMovement>();
        if (movement != null)
        {
            movement.DoManualAttack(skillData.Type, skillData.FirePosition);
        }

        Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);
        Vector3 shotDirection = (skillData.FirePosition - transform.position).normalized;

        FireSkillObject(skillData, startPos, shotDirection);
        currentCooltime = 0.0f;
    }

    public void FireSkillObject(ActiveSkillData skillData, Vector3 startPos, Vector3 skillDir)
    {
        SkillBase skillObject = GamePoolManager.Instance.DequeueSkillPool(skillData.Type);

        if (skillObject == null)
        {
            SkillBase newSkillObjectPrefab = GameDataManager.Instance.GetSkillObjectPrefab(skillData.Type, skillData.ActiveLevel);
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
