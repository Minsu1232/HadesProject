using RPGCharacterAnims.Lookups;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerClass : IPlayerClass
{

    #region ������
    public enum WeaponType
    {
        None,
        GreatSword,
        Sword,
        staff
    }

    public WeaponType weaponType;
    protected IWeapon currentWeapon; // ���� IWeapon Ÿ���� ���
    protected ICharacterAttack characterAttack;

    public PlayerClassData _playerClassData;

    public Rigidbody rb;
    public Transform playerTransform;
    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }
    public int CurrentAttackPower { get; private set; }
    public int CurrentAttackSpeed { get; private set; }
    public int CurrentSpeed { get; private set; }

    public int Level { get; private set; }

    protected bool isDashing = false;
    protected bool isDead = false;

    Animator animator;
    #endregion
    public PlayerClass(PlayerClassData playerClassData, ICharacterAttack characterAttack, Rigidbody rb, Transform playerTransform, Animator animator) // �̵� ���� ������
    {
        _playerClassData = playerClassData;
        InitializeStats();
        this.rb = rb;
        this.playerTransform = playerTransform;
        this.animator = animator;
    }
    public void ChangeWeapon(IWeapon newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log($"Warrior�� {currentWeapon}�� ���⸦ �����߽��ϴ�!");

        // ���� ���濡 ���� �ɷ�ġ�� ������ �� �ֽ��ϴ�.
        SelectWeapon(currentWeapon);
    }
    public void SelectWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        // ���� �̸��� WeaponType���� ��ȯ
        if (Enum.TryParse(weapon.GetType().Name, out WeaponType parsedWeaponType))
        {
            weaponType = parsedWeaponType;
            Debug.Log($"���Ⱑ ����Ǿ����ϴ�: {weaponType}");
        }
        Debug.Log($"���Ⱑ ����Ǿ����ϴ�: {weapon.GetType().Name}");

        // ���⿡ �´� �ִϸ����� �������̵� ����
        currentWeapon.ApplyAnimatorOverride(animator);
    }
    private void InitializeStats()
    {
        CurrentHealth = _playerClassData.initialHp;
        CurrentMana = _playerClassData.initialMp;
        CurrentAttackPower = _playerClassData.initialAttackPower;
        CurrentAttackSpeed = _playerClassData.initialAttackSpeed;
        CurrentSpeed = _playerClassData.initialSpeed;
    }

    // ���� ���� �޼��� �߰�
    protected void ModifyPower(int healthAmount = 0, int manaAmount = 0, int attackAmount = 0, int attackSpeedAmount = 0, int speedAmount = 0)
    {
        CurrentHealth += healthAmount;
        CurrentMana += manaAmount;
        CurrentAttackPower += attackAmount;
        CurrentAttackSpeed += attackSpeedAmount;
        CurrentSpeed += speedAmount;
    }


    // ��ü���� �ൿ�� �ڽ� Ŭ�������� ����
    public abstract void Dash(Vector3 direction);
    public abstract void LevelUp();
    public virtual void Attack()
    {
        characterAttack?.BasicAttack();
    }
    public virtual void UseSkill(int skillIndex)
    {
        characterAttack?.SkillAttack(skillIndex);
    }
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            animator?.SetTrigger("Hit");
        }
    }

    // �⺻ ���� ó���� �����ϰ�, �ڽ� Ŭ�������� �߰� ���� ���� ����
    public virtual void Die()
    {

        if (!isDead)
        {
            isDead = true;
            animator?.SetTrigger("Die");
            Debug.Log("����");
        }

    }
}
