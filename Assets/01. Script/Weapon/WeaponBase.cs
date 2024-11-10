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


    // �޺� ���� ���� (�⺻ 1�ܰ�, 2�ܰ�, 3�ܰ�)
    protected virtual float[] comboMultipliers => new float[] { 1.0f, 1.5f, 2.0f };
    // ������ �⺻ ��ġ�� ȸ����
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
                _weaponInstance.transform.SetParent(parentTransform, false); // �θ� Transform ����
                _weaponInstance.transform.localPosition = DefaultPosition;   // ������ �⺻ ��ġ ����
                _weaponInstance.transform.localEulerAngles = DefaultRotation; // ������ �⺻ ȸ�� ����
                weaponCollider = _weaponInstance.GetComponent<Collider>();
                if (weaponCollider != null)
                {
                    weaponCollider.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
                }

            }
            else
            {
                Debug.LogWarning($"{WeaponName} �ε忡 �����߰ų� handle.Result�� null�Դϴ�.");
            }
        };
        return _weaponInstance;
    }

    public void InitializeWeapon(Animator animator)
    {
        ApplyAnimatorOverride(animator);
        Debug.Log($"{WeaponName}��(��) �����߽��ϴ�.");
    }

    public virtual void ApplyAnimatorOverride(Animator animator)
    {
        Addressables.LoadAssetAsync<AnimatorOverrideController>($"{WeaponName}OverrideController").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                animator.runtimeAnimatorController = handle.Result;
                Debug.Log($"{WeaponName} �ִϸ��̼� �������̵� ��Ʈ�ѷ��� ����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogWarning($"{WeaponName}OverrideController �ε忡 �����߽��ϴ�.");
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
           MonsterStatus monster = other.GetComponent<MonsterStatus>(); // �浹�� ���� ���� ����
            if (monster != null)
            {
                int damage = GetDamage(baseDamage, comboStep);
                monster.TakeDamage(damage);
                OnAttackEffect();
                Debug.Log($"{monster.monsterName}����ü�� {monster.currentHealth}");
            }
        }
       
    }
    public virtual int GetDamage(int _baseDamage, int comboStep)
    {        
        return comboStep * baseDamage;
    }
}
  