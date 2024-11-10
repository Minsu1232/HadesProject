using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterClass
{
    private MonsterData monsterData;

    public int CurrentHealth { get; private set; }
    public int CurrentDeffense { get; private set; }
    public int CurrentAttackPower { get; private set; }
    public int CurrentAttackSpeed { get; private set; }
    public int CurrentSpeed { get; private set; }

    protected bool isDashing = false;
    public MonsterClass(MonsterData data)
    {
        monsterData = data;
        InitializeStats();
    }

    private void InitializeStats()
    {
        CurrentHealth = monsterData.initialHp;
        CurrentAttackPower = monsterData.initialAttackPower;
        CurrentAttackSpeed = monsterData.initialAttackSpeed;
        CurrentSpeed = monsterData.initialSpeed;
    }
    public MonsterData GetMonsterData()
    {
        return monsterData;
    }
    public string GetName()
    {
        return monsterData.MONSTERNAME;
    }
    protected virtual void Debuff()
    {
        GameInitializer.Instance.GetPlayerClass();
    }
    protected void ModifyPower(int healthAmount = 0, int defenseAmount = 0, int attackAmount = 0, int attackSpeedAmount = 0, int speedAmount = 0)
    {
        CurrentHealth += healthAmount;
        CurrentDeffense += defenseAmount;
        CurrentAttackPower += attackAmount;
        CurrentAttackSpeed += attackSpeedAmount;
        CurrentSpeed += speedAmount;
    }
    public abstract void Attack();  // 공격 메서드 정의
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }
    public abstract void Die();
    

    public virtual void SetPosition(Vector3 spawnPosition)
    {
        throw new NotImplementedException();
    }
}
