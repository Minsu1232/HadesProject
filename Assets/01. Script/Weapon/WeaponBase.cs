using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    public virtual string WeaponName { get; }

    public virtual string AnimationName { get; }

    public virtual string AnimationName1 { get; }

    public virtual string AnimationName2 { get; }

    public virtual string AnimationName3 { get; }
    public virtual int baseDamage { get; }
    private int comboStep = 0;


    // 콤보 배율 설정 (기본 1단계, 2단계, 3단계)
    protected virtual float[] comboMultipliers => new float[] { 1.0f, 1.5f, 2.0f };
    // 무기의 기본 위치와 회전값
    protected virtual Vector3 DefaultPosition => Vector3.zero;
    protected virtual Vector3 DefaultRotation => Vector3.zero;

   

    protected GameObject _weaponInstance;
    private Collider weaponCollider;

   
    public virtual void ActivateCollider(int comboStep)
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
            this.comboStep = comboStep;
        }
    }
    public virtual void DeactivateCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }
    public GameObject WeaponLoad(Transform parentTransform)
    {
        Addressables.LoadAssetAsync<GameObject>(WeaponName).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                _weaponInstance = GameObject.Instantiate(handle.Result);
                _weaponInstance.transform.SetParent(parentTransform, false); // 부모 Transform 설정
                _weaponInstance.transform.localPosition = DefaultPosition;   // 무기의 기본 위치 설정
                _weaponInstance.transform.localEulerAngles = DefaultRotation; // 무기의 기본 회전 설정
                weaponCollider = _weaponInstance.GetComponent<Collider>();
                if (weaponCollider != null)
                {
                    weaponCollider.enabled = false; // 초기에는 비활성화
                }

            }
            else
            {
                Debug.LogWarning($"{WeaponName} 로드에 실패했거나 handle.Result가 null입니다.");
            }
        };
        return _weaponInstance;
    }

    public void InitializeWeapon(Animator animator)
    {
        ApplyAnimatorOverride(animator);
        Debug.Log($"{WeaponName}을(를) 장착했습니다.");
    }

    public virtual void ApplyAnimatorOverride(Animator animator)
    {
        Addressables.LoadAssetAsync<AnimatorOverrideController>($"{WeaponName}OverrideController").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                animator.runtimeAnimatorController = handle.Result;
                Debug.Log($"{WeaponName} 애니메이션 오버라이드 컨트롤러가 적용되었습니다.");
            }
            else
            {
                Debug.LogWarning($"{WeaponName}OverrideController 로드에 실패했습니다.");
            }
        };
    }

    public abstract void OnAttackEffect();
    public abstract void SpecialAttack();
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called on {gameObject.name} with ID: {GetInstanceID()}");
        if (other.CompareTag("Monster"))
        {
           MonsterStatus monster = other.GetComponent<MonsterStatus>(); // 충돌한 몬스터 직접 참조
            if (monster != null)
            {
                int damage = GetDamage(baseDamage, comboStep);
                monster.TakeDamage(damage);
                OnAttackEffect();
                Debug.Log($"{monster.monsterName}남은체력 {monster.currentHealth}");
            }
        }
       
    }
    public virtual int GetDamage(int _baseDamage, int comboStep)
    {        
        return comboStep * baseDamage;
    }
}
  