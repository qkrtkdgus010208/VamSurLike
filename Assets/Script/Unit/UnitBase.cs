using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
    public int TotalHp = 0;
    public int Hp = 0;
    public int Power = 0;
    public int Armor = 0;
}

public class UnitBase : MonoBehaviour
{
    public bool IsAlive { private set; get; }
    public UnitData UnitData { private set; get; }
    public int UnitId { private set; get; }

    void Start()
    {
        
    }

    public virtual void InitUnit(int unitId, int hp, int power, int armor)
    {
        UnitId = unitId;
        UnitData = new UnitData();
        UnitData.TotalHp = UnitData.Hp = hp;
        UnitData.Power = power;
        UnitData.Armor = armor;
    }

    public virtual void OnHit(int damage)
    {
        if (UnitData != null)
        {
            return;
        }
    }

    public virtual void OnDie()
    {

    }
}
