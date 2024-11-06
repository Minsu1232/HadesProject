using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarriorClass : PlayerClass
{
    private int rage; // �г� ������, ���� �� ���� ������ ��ų ��ȭ�� ��� ����
    
    public enum WarriorType
    {

        Sword,
        Axe

    }
    public WarriorClass(PlayerClassData data, Rigidbody rb, Transform playerTransform)
        : base(data, rb, playerTransform) 
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
    public override void UseSkill()
    {
       
    }
    
    // Warrior���� ���� ��ȭ
    public override void TakeDamage(int damage)
    {
        int reducedDamage = Mathf.Max(0, damage - CurrentDeffense); // ���¿� ���� ���� ����
        base.TakeDamage(reducedDamage); // ���� ���� �� ó��
        Debug.Log($"Warrior�� {reducedDamage} ���ظ� �޾ҽ��ϴ�!");
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
