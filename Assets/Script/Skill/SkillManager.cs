using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
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
            newSkillData.FirePosition = hit.point;
            FireSkill(newSkillData);
        }
    }

    public void FireSkill(ActiveSkillData skillData)
    {
        Vector3 startPos = new Vector3(transform.position.x, 1, transform.position.z);
        Vector3 shotDirection = (skillData.FirePosition - transform.position).normalized;

        FireSkillObject(skillData, startPos, shotDirection);
    }

    public void FireSkillObject(ActiveSkillData skillData, Vector3 startPos, Vector3 skillDir)
    {
        SkillBase skillObject = GamePoolManager.Instance.DequeueSkillPool(skillData.Type);

        if (skillObject == null)
        {
            SkillBase newSkillObjectPrefab = Resources.Load<SkillBase>("Prefabs/Missile");
            Instantiate(newSkillObjectPrefab, startPos, Quaternion.identity, GameDataManager.Instance.GetSkillRootTransform());

            if (skillObject == null)
            {
                return;
            }
        }
    }
}
