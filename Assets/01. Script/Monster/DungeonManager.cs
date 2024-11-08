using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ���� ���� ���� ��ũ��Ʈ
/// </summary>
public class DungeonManager : Singleton<DungeonManager>
{
    private MonsterFactoryBase monsterFactory;
    private MonsterClass currentMonster; // ���� ������ ���͸� �����ϴ� �ʵ�
    [SerializeField] private Transform player; // �÷��̾��� Transform ����

    private void Start()
    {
        // ���� �׸��� ���� ������ ���丮 ����
        monsterFactory = GetMonsterFactoryForDungeon("test");

        player = GameObject.FindGameObjectWithTag("Player").transform;
        // �׽�Ʈ�� ���� ����
        SpawnMonster(new Vector3(0, 3, 0));
    }

    private MonsterFactoryBase GetMonsterFactoryForDungeon(string dungeonType)
    {
        switch (dungeonType)
        {
            //case "Forest":
            //    return new ForestMonsterFactory();
            case "test":
                return new DummyMonsterFactory();
            default:
                Debug.LogError("�� �� ���� ���� Ÿ���Դϴ�.");
                return null;
        }
    }

    private void SpawnMonster(Vector3 spawnPosition)
    {
        monsterFactory.CreateMonster(spawnPosition, monster =>
        {
            Debug.Log($"Monster {monster.GetName()} ���� �Ϸ�");
            // ���� ������ ���͸� �ʵ忡 ����
            currentMonster = monster;
                       
            // �߰� ���� ����
        });
    }
    public MonsterClass GetMonsterClass()
    {
        return currentMonster;
    }
    // Player Transform�� ��ȯ�ϴ� �޼���
    public Transform GetPlayerTransform()
    {
        return player;
    }
}