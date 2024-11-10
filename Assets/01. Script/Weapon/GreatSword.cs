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

    // 무기의 위치와 회전을 재정의하여 캐릭터의 손에 정확히 배치합니다.
    protected override Vector3 DefaultPosition => new Vector3(-0.096f, -0.133f, 0.2663f);
    protected override Vector3 DefaultRotation => new Vector3(8.5f, -3.539f, 159.05f);
    public override void OnAttackEffect()
    {
        Debug.Log("대검 공격으로 적에게 출혈 효과 적용!");
    }

    public override void SpecialAttack()
    {
        Debug.Log("대검의 특수 스킬 발동!");
    }
}

// Update is called once per frame
