using RPGCharacterAnims.Lookups;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerClass : IPlayerClass
{

    #region 변수들
    public enum WeaponType
    {
        None,
        GreatSword,
        Sword,
        staff
    }

    public WeaponType weaponType;
    protected IWeapon currentWeapon; // 공통 IWeapon 타입을 사용
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
    public PlayerClass(PlayerClassData playerClassData, ICharacterAttack characterAttack, Rigidbody rb, Transform playerTransform, Animator animator) // 이동 관련 생성자
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
        Debug.Log($"Warrior가 {currentWeapon}로 무기를 변경했습니다!");

        // 무기 변경에 따른 능력치를 갱신할 수 있습니다.
        SelectWeapon(currentWeapon);
    }
    public void SelectWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        // 무기 이름을 WeaponType으로 변환
        if (Enum.TryParse(weapon.GetType().Name, out WeaponType parsedWeaponType))
        {
            weaponType = parsedWeaponType;
            Debug.Log($"무기가 변경되었습니다: {weaponType}");
        }
        Debug.Log($"무기가 변경되었습니다: {weapon.GetType().Name}");

        // 무기에 맞는 애니메이터 오버라이딩 적용
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

    // 상태 변경 메서드 추가
    protected void ModifyPower(int healthAmount = 0, int manaAmount = 0, int attackAmount = 0, int attackSpeedAmount = 0, int speedAmount = 0)
    {
        CurrentHealth += healthAmount;
        CurrentMana += manaAmount;
        CurrentAttackPower += attackAmount;
        CurrentAttackSpeed += attackSpeedAmount;
        CurrentSpeed += speedAmount;
    }


    // 구체적인 행동은 자식 클래스에서 구현
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

    // 기본 죽음 처리만 정의하고, 자식 클래스에서 추가 로직 구현 가능
    public virtual void Die()
    {

        if (!isDead)
        {
            isDead = true;
            animator?.SetTrigger("Die");
            Debug.Log("죽음");
        }

    }
}
