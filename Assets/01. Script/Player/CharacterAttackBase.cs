using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttackBase : MonoBehaviour, ICharacterAttack
{
    protected IWeapon currentWeapon;
    protected Animator animator;
    public int comboStep = 0; // �޺� �ܰ踦 ����
    public bool canCombo = false; // �޺��� �������� ����
    int hashAttackCount = Animator.StringToHash("AttackCount");
    bool isAttacking = false;
    public int AttackCount { get => animator.GetInteger(hashAttackCount); set => animator.SetInteger(hashAttackCount, value); }
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GameInitializer.Instance.GetCurrentWeapon();
    }

    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        if (currentWeapon != null)
        {
            currentWeapon.InitializeWeapon(animator);
            Debug.Log($"CharacterAttackBase�� {currentWeapon.WeaponName} ���Ⱑ �����Ǿ����ϴ�.");
        }
    }

    private void Update()
    {
        // �޺� �ܰ� Ȯ���� ���� ����� �α�
        Debug.Log(comboStep);
    }

    // ������ �����ϴ� �޼���: �ִϸ��̼� Ʈ���Ÿ� �����Ͽ� ������ ����
    public virtual void BasicAttack()
    {
       
            
            // ù ��° ������ ��� comboStep�� �ʱ�ȭ        
         
            // �޺��� �´� �ݶ��̴� Ȱ��ȭ
            currentWeapon?.ActivateCollider(comboStep);

            // �ִϸ��̼� Ʈ���� ����
            animator.SetTrigger("Attack");
           

            canCombo = false; // �޺� �ߺ� ����
            Debug.Log("BasicAttack ����: " + comboStep);
            
        
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��: �ֵθ��� ���� �� �ݶ��̴� Ȱ��ȭ
    public void ActivateCollider()
    {
        currentWeapon?.ActivateCollider(comboStep);
        Debug.Log("Collider Ȱ��ȭ��");
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��: �ֵθ��� �� �� �ݶ��̴� ��Ȱ��ȭ
    public void DeactivateCollider()
    {
        currentWeapon?.DeactivateCollider();
        Debug.Log("Collider ��Ȱ��ȭ��");
    }   

    public virtual void SkillAttack(int skillIndex)
    {
        throw new System.NotImplementedException();
    }
    public void AttackFinished(int step)
    {
        comboStep = step;  // ���޹��� step ���� comboStep�� �Ҵ�
       
    }
}
    // �޺��� �ʱ�ȭ
  
   

    

