using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPcUnitMovement : UnitMovementBase
{
    void Start()
    {
        GameControl.Instance.OnMove += HandleMove;
        GameControl.Instance.OnMoveStart += HandleMoveStart;
        GameControl.Instance.OnMoveEnd += HandleMoveEnd;
    }

    void OnDestroy()
    {
        GameControl.Instance.OnMove -= HandleMove;
        GameControl.Instance.OnMoveStart -= HandleMoveStart;
        GameControl.Instance.OnMoveEnd -= HandleMoveEnd;
    }

    public void DoManualAttack(SkillType skillType, Vector3 attackPos)
    {
        Vector3 attackDirect = (attackPos - transform.position).normalized;
        rotationTransform.rotation = Quaternion.RotateTowards(rotationTransform.rotation, Quaternion.LookRotation(attackDirect), 360);
    }

    private void HandleMove(Vector3 direct)
    {
        // 이동
        transform.position += direct * speed * Time.deltaTime;

        //회전
        rotationTransform.rotation = Quaternion.RotateTowards(rotationTransform.rotation, Quaternion.LookRotation(direct), rotationSpeed * Time.deltaTime);
    }

    private void HandleMoveStart()
    {
        if (animator != null)
        {
            animator.CrossFade("Run", 0.1f);
        }
    }

    private void HandleMoveEnd()
    {
        if (animator != null)
        {
            animator.CrossFade("Idle", 0.1f);
        }
    }
}
