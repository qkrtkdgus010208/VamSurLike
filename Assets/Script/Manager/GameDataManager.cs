using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

    public int Stage { get; private set; }

    private GameObject MyPc;
    private Transform SpawnRoot;
    private Transform SkillRoot;
    private Transform ItemRoot;

    private Dictionary<SkillType, SkillData> SkillDatas;
    private Dictionary<string, SkillBase> SkillResources;

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

        SkillDatas.Clear();
        SkillDatas = null;

        SkillResources.Clear();
        SkillResources = null;
    }

    public void LoadAll()
    {
        LoadSkillData();
    }

    public void LoadSkillData()
    {
        SkillDatas = new Dictionary<SkillType, SkillData>();
        SkillResources = new Dictionary<string, SkillBase>();
        SkillDatas.Clear();
        SkillResources.Clear();

        TextAsset skillJsonTextAsset = Resources.Load<TextAsset>("Data/SkillDatas");
        string skillJson = skillJsonTextAsset.text;
        JObject dataObject = JObject.Parse(skillJson);
        JToken token = dataObject["Skills"];
        JArray array = token.Value<JArray>();

        foreach (JObject eachObject in array)
        {
            SkillData newSkillData = new SkillData();
            newSkillData.Type = Enum.Parse<SkillType>(eachObject.Value<string>("Type"));
            newSkillData.ActiveType = Enum.Parse<SkillActiveType>(eachObject.Value<string>("ActiveType"));
            newSkillData.LevelDatas = new Dictionary<int, SkillLevelData>();
            JArray levelArray = eachObject.Value<JArray>("LevelDatas");

            foreach (JObject eachLevel in levelArray)
            {
                SkillLevelData newSkillLevelData = new SkillLevelData();
                newSkillLevelData.Type = newSkillData.Type;
                newSkillLevelData.Level = eachLevel.Value<int>("Level");
                newSkillLevelData.Path = eachLevel.Value<string>("Path");
                newSkillLevelData.Power = eachLevel.Value<int>("Power");
                newSkillLevelData.Size = eachLevel.Value<int>("Size");
                newSkillLevelData.Speed = eachLevel.Value<float>("Speed");
                newSkillLevelData.ActiveTime = eachLevel.Value<float>("ActiveTime");
                newSkillLevelData.Cooltime = eachLevel.Value<float>("Cooltime");

                newSkillData.LevelDatas.Add(newSkillLevelData.Level, newSkillLevelData);

                SkillBase skillObject = Resources.Load<SkillBase>(newSkillLevelData.Path);
                string skillId = GetSkillId(newSkillLevelData);
                SkillResources.Add(skillId, skillObject);
            }

            SkillDatas.Add(newSkillData.Type, newSkillData);
        }
    }   

    public string GetSkillId(SkillLevelData skillLevelData)
    {
        return GetSkillId(skillLevelData.Type, skillLevelData.Level);
    }

    public string GetSkillId(SkillType skillType, int level)
    {
        return string.Format("{0}_{1}", skillType.ToString(), level);
    }

    public SkillData FindSkillData(SkillType skillType)
    {
        if (SkillDatas.ContainsKey(skillType) == false)
        {
            return null;
        }

        return SkillDatas[skillType];
    }

    public SkillLevelData FindSkillLevelData(SkillType skillType, int skillLevel)
    {
        if (SkillDatas.ContainsKey(skillType) == false)
        {
            return null;
        }

        if (SkillDatas[skillType].LevelDatas.ContainsKey(skillLevel) == false)
        {
            return null;
        }

        return SkillDatas[skillType].LevelDatas[skillLevel];
    }

    public SkillBase GetSkillObjectPrefab(SkillLevelData skillLevelData)
    {
        return GetSkillObjectPrefab(GetSkillId(skillLevelData));
    }

    public SkillBase GetSkillObjectPrefab(SkillType type, int skillLevel)
    {
        return GetSkillObjectPrefab(GetSkillId(type, skillLevel));
    }

    public SkillBase GetSkillObjectPrefab(string skillId)
    {
        if (SkillResources.ContainsKey(skillId) == false)
        {
            return null;
        }

        return SkillResources[skillId];
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
