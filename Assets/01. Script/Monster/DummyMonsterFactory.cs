using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DummyMonsterFactory : MonsterFactoryBase
{
    public override MonsterClass CreateMonster(Vector3 spawnPosition, System.Action<MonsterClass> onMonsterCreated)
    {
        {
            Addressables.LoadAssetAsync<MonsterData>("TestData(DummyMonster)").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    MonsterData monsterData = handle.Result;
                    monsterData.monsterPrefab.InstantiateAsync(spawnPosition, Quaternion.identity).Completed += prefabHandle =>
                    {
                        if (prefabHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            GameObject monsterObject = prefabHandle.Result;
                            MonsterClass monster = new DummyMonster(monsterData);
                            onMonsterCreated?.Invoke(monster);
                        }
                    };
                }
            };

            // ���̷� ������ ��ü ��ȯ (���� ��ȯ �Ϸ�� �񵿱� �ݹ鿡�� ó����)
            return null;
        }
    }
}



