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

    public void Init()
    {

    }

    public void Clear()
    {

    }
}
