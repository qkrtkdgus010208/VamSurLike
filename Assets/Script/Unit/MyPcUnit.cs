using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPcUnit : UnitBase
{
    public int MaxExp { get; set; }
    public int Exp { get; set; }
    public int Level { get; set; }

    private const int MAX_EXP_FROM_LEVEL_VALUE = 10000;

    void Start()
    {
        OnDie();
    }

    public override void InitUnit(int unitId, int hp, int power, int armor)
    {
        base.InitUnit(unitId, hp, power, armor);
        Exp = 0;
        MaxExp = MAX_EXP_FROM_LEVEL_VALUE;
        Level = 1;
    }

    public void SetUpLevel(int level)
    {
        Exp = 0;
        MaxExp = MAX_EXP_FROM_LEVEL_VALUE * Level;
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);
    }

    public override void OnDie()
    {
        base.OnDie();
    }

    void Update()
    {
        
    }
}
