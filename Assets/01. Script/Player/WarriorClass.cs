using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarriorClass : PlayerClass
{
    private int rage; // �г� ������, ���� �� ���� ������ ��ų ��ȭ�� ��� ����

    public enum WeaponType
    {   
        None,
        Sword,
        Axe
    }

    private WeaponType currentWeapon = WeaponType.None;
    public WarriorClass(PlayerClassData data, ICharacterAttack characterAttack, Rigidbody rb, Transform playerTransform, Animator animator)
        : base(data,characterAttack ,rb, playerTransform, animator) 
    {
        rage = 0;
    }
    public override void LevelUp()
    {
       
    }
    public override void Attack()
    {
        Debug.Log("Warrior�� ���� ������ �մϴ�!");
        // ���� ���� ���� �߰�
        ModifyPower(attackAmount: (rage / 10)); // �г� �������� �߰� ���ط� Ȱ��
        rage += 5; // ���� �� �г� ����
    }

    // Warrior�� ��ų ��� ���
    public override void UseSkill(int index)
    {
       
    }
    public void ChangeWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log($"Warrior�� {currentWeapon}�� ���⸦ �����߽��ϴ�!");
        // ���� ���濡 ���� �ִϸ��̼��̳� �ɷ�ġ�� ������ �� �ֽ��ϴ�.
    }
    // Warrior���� ���� ��ȭ
    public override void TakeDamage(int damage)
    {        
        base.TakeDamage(damage); // ���� ���� �� ó��
        Debug.Log($"Warrior�� {damage} ���ظ� �޾ҽ��ϴ�!");
    }
    public override void Dash(Vector3 direction)
    {
        if (isDashing) return;
        isDashing = true;

        Vector3 dashTarget = rb.position + direction; // ���� ��� ��ǥ ����
       

        rb.DOMove(dashTarget, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
               
                isDashing = false;
            });
    }
    public override void Die()
    {
        base.Die();
        Debug.Log("Warrior�� �����߽��ϴ�. ���� ���� ����Ʈ�� ǥ���մϴ�.");
        // Warrior���� ���� ó�� ���� �߰� ����
    }
}
