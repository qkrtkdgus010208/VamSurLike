using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance;

    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameDataManager();
            }

            return instance;
        }
    }

    public GameObject GetMyPcObject()
    {
        return mMyPc;
    }

    public Transform GetSpawnRootTransform()
    {
        return mSpawnRoot;
    }

    public Transform GetItemRootTransform()
    {
        return mItemRoot;
    }

    public Transform GetSkillRootTransform()
    {
        return mSkillRoot;
    }

    public int mStage { get; private set; }

    private GameObject mMyPc;
    private Transform mSpawnRoot;
    private Transform mSkillRoot;
    private Transform mItemRoot;

    public void Init()
    {

    }

    public void Clear()
    {
        mStage = 0;
        mMyPc = null;
        mSpawnRoot = null;
        mSkillRoot = null;
        mItemRoot = null;
    }

    public void SetStageData(GameObject InMyPc, Transform InSpawnRoot, Transform InSkillRoot, Transform InItemRoot)
    {
        mMyPc = InMyPc;
        mSpawnRoot = InSpawnRoot;  
        mSkillRoot = InSkillRoot;   
        mItemRoot = InItemRoot;

    }

    public void SetCurrentStage(int stage)
    {
        mStage = stage;
    }
}
