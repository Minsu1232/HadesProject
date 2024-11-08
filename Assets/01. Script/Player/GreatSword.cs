using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GreatSword : MonoBehaviour,IWeapon
// Start is called before the first frame update
{
    public string WeaponName => "GreatSword";

    public void InitializeWeapon(Animator animator)
    {
        ApplyAnimatorOverride(animator);
        Debug.Log("대검을 장착했습니다.");
    }

    public void ApplyAnimatorOverride(Animator animator)
    {
        // Addressables로 애니메이션 오버라이드 컨트롤러 로드
        Addressables.LoadAssetAsync<AnimatorOverrideController>("GreatSwordOverrideController").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                AnimatorOverrideController overrideController = handle.Result;
                animator.runtimeAnimatorController = overrideController;
                Debug.Log("대검 애니메이션 오버라이드 컨트롤러가 적용되었습니다.");
            }
            else
            {
                Debug.LogWarning("GreatSwordOverrideController 로드에 실패했습니다.");
            }
        };
    }

    public void OnAttackEffect()
    {
        Debug.Log("대검 공격으로 적에게 출혈 효과 적용!");
        // 출혈 효과 구현 (예: 적에게 DOT(Damage Over Time) 효과 부여)
    }

    public void SpecialAttack()
    {
        Debug.Log("대검의 특수 스킬 발동!");
        // 대검의 특수 스킬 로직 추가
    }
}

// Update is called once per frame
