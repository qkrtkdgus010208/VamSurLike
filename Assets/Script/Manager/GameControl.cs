using UnityEngine;

public class GameControl
{
    private static GameControl instance;

    public static GameControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameControl();
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
