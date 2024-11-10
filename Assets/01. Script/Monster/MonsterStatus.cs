using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    private MonsterClass monsterClass; // ������ ������
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
        Debug.Log($"{monsterName}�� {damage} �������� �޾ҽ��ϴ�. ���� ü��: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{monsterName} óġ��.");
        Destroy(gameObject); // ���� ������Ʈ ����
    }
}
