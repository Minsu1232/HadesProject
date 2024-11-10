using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GreatSword : WeaponBase
// Start is called before the first frame update
{
    public override string WeaponName => "GreatSword";

    public override string AnimationName { get; }

    public override string AnimationName1 { get; }

    public override string AnimationName2 { get; }

    public override string AnimationName3 { get; }
    public override int baseDamage => 20;

    public int GetDamage(int comboStep)
    {
        float multiplier = comboStep > 0 && comboStep <= comboMultipliers.Length
                           ? comboMultipliers[comboStep - 1]
                           : 1.0f;
        return Mathf.RoundToInt(baseDamage * multiplier);
    }

    // ������ ��ġ�� ȸ���� �������Ͽ� ĳ������ �տ� ��Ȯ�� ��ġ�մϴ�.
    protected override Vector3 DefaultPosition => new Vector3(-0.096f, -0.133f, 0.2663f);
    protected override Vector3 DefaultRotation => new Vector3(8.5f, -3.539f, 159.05f);
    public override void OnAttackEffect()
    {
        Debug.Log("��� �������� ������ ���� ȿ�� ����!");
    }

    public override void SpecialAttack()
    {
        Debug.Log("����� Ư�� ��ų �ߵ�!");
    }
}

// Update is called once per frame
