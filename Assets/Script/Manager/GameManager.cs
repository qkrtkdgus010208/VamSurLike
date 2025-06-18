using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mStageId = 1;
    public GameObject MyPc;
    public Transform NpcSpawnParent;
    public Transform SkillObjectParent;
    public Transform ItemObjectParent;

    void Start()
    {
        GameDataManager.Instance.Init();
        GameDataManager.Instance.SetStageData(MyPc, NpcSpawnParent, SkillObjectParent, ItemObjectParent);
        GameDataManager.Instance.SetCurrentStage(mStageId);
        GameDataManager.Instance.LoadAll();

        GamePoolManager.Instance.Init();

        GameControl.Instance.Init();
        GameControl.Instance.SetControlObject(MyPc);

        SpawnManager.Instance.Init();
        FSMStageController.Instance.Init();

        FSMStageController.Instance.EnterStage();
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
        FSMStageController.Instance.OnUpdate(Time.deltaTime);

        GameControl.Instance.OnUpdate();
    }
}
