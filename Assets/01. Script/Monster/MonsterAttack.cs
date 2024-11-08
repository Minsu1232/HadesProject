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
        player = DungeonManager.Instance.GetPlayerTransform();  // 플레이어 참조 가져오기
       

        // DungeonManager를 통해 MonsterData에서 공격 범위와 쿨다운 설정
        MonsterData monsterData = DungeonManager.Instance.GetMonsterClass().GetMonsterData();
        attackRange = monsterData.attackRange;
        attackCooldown = monsterData.attackCooldown;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 공격 범위 내에 있고 쿨다운이 끝났으면 공격
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log($"{monsterClass.GetName()}이 {attackRange} 범위 내에서 {playerClass._playerClassData.classType}을 공격합니다!");
        // 애니메이션이 있는 경우 여기서 트리거 설정
        // 예: animator.SetTrigger("Attack");

        ApplyDamage();
    }

    public void ApplyDamage()
    {
        playerClass.TakeDamage(monsterClass.CurrentAttackPower);
    }
}
