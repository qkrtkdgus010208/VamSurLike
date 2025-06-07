using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mStageId = 1;
    public GameObject mMyPc;
    public Transform mNpcSpawnParent;
    public Transform mSkillObjectParent;
    public Transform mItemObjectParent;

    void Start()
    {
        GamePoolManager.Instance.Init();
        GameControl.Instance.Init();
        SpawnManager.Instance.Init();
        FSMStageController.Instance.Init();
    }

    void OnDestroy()
    {
        GamePoolManager.Instance.Clear();
        GameControl.Instance.Clear();
        SpawnManager.Instance.Clear();
        FSMStageController.Instance.Clear();
    }

    void Update()
    {
        
    }
}
