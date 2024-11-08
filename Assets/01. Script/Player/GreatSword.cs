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
        Debug.Log("����� �����߽��ϴ�.");
    }

    public void ApplyAnimatorOverride(Animator animator)
    {
        // Addressables�� �ִϸ��̼� �������̵� ��Ʈ�ѷ� �ε�
        Addressables.LoadAssetAsync<AnimatorOverrideController>("GreatSwordOverrideController").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                AnimatorOverrideController overrideController = handle.Result;
                animator.runtimeAnimatorController = overrideController;
                Debug.Log("��� �ִϸ��̼� �������̵� ��Ʈ�ѷ��� ����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogWarning("GreatSwordOverrideController �ε忡 �����߽��ϴ�.");
            }
        };
    }

    public void OnAttackEffect()
    {
        Debug.Log("��� �������� ������ ���� ȿ�� ����!");
        // ���� ȿ�� ���� (��: ������ DOT(Damage Over Time) ȿ�� �ο�)
    }

    public void SpecialAttack()
    {
        Debug.Log("����� Ư�� ��ų �ߵ�!");
        // ����� Ư�� ��ų ���� �߰�
    }
}

// Update is called once per frame
