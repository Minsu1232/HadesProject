using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Monster")]
public class MonsterData : ScriptableObject
{
    public string MONSTERNAME;
    public int initialHp;   
    public int initialAttackPower;
    public int initialAttackSpeed;
    public int initialSpeed;
    public float attackRange;
    public float attackCooldown;

    public MonsterType monsterType;

    public AssetReferenceGameObject monsterPrefab; // Addressable ������ ���۷���

    public enum MonsterType
    {

        Warrior,
        Magician

    }
}

