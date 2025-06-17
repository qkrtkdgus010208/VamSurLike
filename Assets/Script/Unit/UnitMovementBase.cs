using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementBase : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform rotationTransform;
    public float rotationSpeed = 400.0f;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        // 높이 계산
        Vector3 nowPostion = transform.position + new Vector3(0, 100, 0);
        Vector3 direction = new Vector3(0, -1, 0);

        int layermask = 1 << LayerMask.NameToLayer("Terrain");

        if (Physics.Raycast(nowPostion, direction, out RaycastHit hit, 200, layermask))
        {
            float height = hit.point.y;
            Vector3 newPos = transform.position;
            newPos.y = height;
            transform.position = newPos;
        }
    }
}
