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
        GameDataManager.Instance.Init();
        GameDataManager.Instance.SetStageData(mMyPc, mNpcSpawnParent, mSkillObjectParent, mItemObjectParent);
        GameDataManager.Instance.SetCurrentStage(mStageId);

        GamePoolManager.Instance.Init();
        GameControl.Instance.Init();
        SpawnManager.Instance.Init();
        FSMStageController.Instance.Init();
    }

    void OnDestroy()
    {
        GameDataManager.Instance.Clear();

        GamePoolManager.Instance.Clear();
        GameControl.Instance.Clear();
        SpawnManager.Instance.Clear();
        FSMStageController.Instance.Clear();
    }

    void Update()
    {
        
    }
}
