using UnityEngine;

public class SpawnManager
{
    private static SpawnManager instance;

    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpawnManager();
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
