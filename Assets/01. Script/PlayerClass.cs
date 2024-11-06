using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerClass : IPlayerClass
{
    public PlayerClassData _playerClassData;

    public Rigidbody rb;
    public Transform playerTransform;
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }
    public int CurrentDeffense { get; private set; }
    public int CurrentAttackPower { get; private set; }
    public int CurrentAttackSpeed { get; private set; }
    public int CurrentSpeed { get; private set; }

    public int Level { get; private set; }

    protected bool isDashing = false;
   
    public PlayerClass(PlayerClassData playerClassData, Rigidbody rb, Transform playerTransform) // �̵� ���� ������
    {
        _playerClassData = playerClassData;
        InitializeStats();
        this.rb = rb;
        this.playerTransform = playerTransform;
       
    }
    private void InitializeStats()
    {
        CurrentHealth = _playerClassData.initialHp;
        CurrentMana = _playerClassData.initialMp;
        CurrentDeffense = _playerClassData.initialDeffense;
        CurrentAttackPower = _playerClassData.initialAttackPower;
        CurrentAttackSpeed = _playerClassData.initialAttackSpeed;
        CurrentSpeed = _playerClassData.initialSpeed;
    }
    
    // ���� ���� �޼��� �߰�
    protected void ModifyPower(int healthAmount = 0,int manaAmount = 0,int defenseAmount = 0,int attackAmount = 0, int attackSpeedAmount = 0, int speedAmount = 0)
    {
        CurrentHealth += healthAmount;
        CurrentMana += manaAmount;
        CurrentDeffense += defenseAmount;
        CurrentAttackPower += attackAmount;
        CurrentAttackSpeed += attackSpeedAmount;
        CurrentSpeed += speedAmount;
    }


    // ��ü���� �ൿ�� �ڽ� Ŭ�������� ����
    public abstract void Dash(Vector3 direction); 
    public abstract void LevelUp();
    public abstract void Attack();
    public abstract void UseSkill();
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    // �⺻ ���� ó���� �����ϰ�, �ڽ� Ŭ�������� �߰� ���� ���� ����
    public virtual void Die()
    {
        Debug.Log("����");
    }
}
