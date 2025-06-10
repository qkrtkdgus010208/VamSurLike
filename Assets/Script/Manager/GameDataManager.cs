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
        return MyPc;
    }

    public Transform GetSpawnRootTransform()
    {
        return SpawnRoot;
    }

    public Transform GetItemRootTransform()
    {
        return ItemRoot;
    }

    public Transform GetSkillRootTransform()
    {
        return SkillRoot;
    }

    public int Stage { get; private set; }

    private GameObject MyPc;
    private Transform SpawnRoot;
    private Transform SkillRoot;
    private Transform ItemRoot;

    public void Init()
    {

    }

    public void Clear()
    {
        Stage = 0;
        MyPc = null;
        SpawnRoot = null;
        SkillRoot = null;
        ItemRoot = null;
    }

    public void SetStageData(GameObject inMyPc, Transform inSpawnRoot, Transform inSkillRoot, Transform inItemRoot)
    {
        MyPc = inMyPc;
        SpawnRoot = inSpawnRoot;  
        SkillRoot = inSkillRoot;   
        ItemRoot = inItemRoot;

    }

    public void SetCurrentStage(int stage)
    {
        Stage = stage;
    }
}
