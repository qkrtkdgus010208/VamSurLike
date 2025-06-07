using UnityEngine;

public class GamePoolManager
{
    private static GamePoolManager instance;

    public static GamePoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GamePoolManager();
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
