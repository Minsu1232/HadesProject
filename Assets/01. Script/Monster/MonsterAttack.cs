using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private float attackRange;
    private float attackCooldown;
    private float lastAttackTime;
    private Transform player;
    private PlayerClass playerClass;
    private MonsterClass monsterClass;

    private void Start()
    {   
        playerClass = GameInitializer.Instance.GetPlayerClass();
        monsterClass = DungeonManager.Instance.GetMonsterClass();
        player = DungeonManager.Instance.GetPlayerTransform();  // �÷��̾� ���� ��������
       

        // DungeonManager�� ���� MonsterData���� ���� ������ ��ٿ� ����
        MonsterData monsterData = DungeonManager.Instance.GetMonsterClass().GetMonsterData();
        attackRange = monsterData.attackRange;
        attackCooldown = monsterData.attackCooldown;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ���� ���� �ְ� ��ٿ��� �������� ����
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log($"{monsterClass.GetName()}�� {attackRange} ���� ������ {playerClass._playerClassData.classType}�� �����մϴ�!");
        // �ִϸ��̼��� �ִ� ��� ���⼭ Ʈ���� ����
        // ��: animator.SetTrigger("Attack");

        ApplyDamage();
    }

    public void ApplyDamage()
    {
        playerClass.TakeDamage(monsterClass.CurrentAttackPower);
    }
}
