using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    private MonsterClass monsterClass; // 데이터 관리용
    public string monsterName;
    public int currentHealth;
    public int maxHealth;

    public void Initialize(MonsterClass data)
    {
        monsterClass = data;
        monsterName = monsterClass.GetName();
        maxHealth = monsterClass.GetMonsterData().initialHp;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{monsterName}가 {damage} 데미지를 받았습니다. 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{monsterName} 처치됨.");
        Destroy(gameObject); // 몬스터 오브젝트 삭제
    }
}
