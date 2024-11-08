using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttackBase : MonoBehaviour, ICharacterAttack
{
    protected IWeapon currentWeapon;
    protected Animator animator;
    public int comboStep = 0;
    public bool canCombo = false; // �޺��� �������� ����
    private float comboWindow = 0.5f; // ���� �޺� �Է��� ������ �ð�(��: 0.5��)
    protected float lastAttackTime;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GameInitializer.Instance.GetCurrentWeapon(); // ���� ������ ���⸦ ������
    }
    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        if (currentWeapon != null)
        {
            currentWeapon.InitializeWeapon(animator); // ���� �ʱ�ȭ �� �ִϸ��̼� �������̵�
            Debug.Log($"CharacterAttackBase�� {currentWeapon.WeaponName} ���Ⱑ �����Ǿ����ϴ�.");
        }
    }
    public virtual void BasicAttack()
    {
        // �޺��� �����ϰų� ù ��° ������ ���� ������ ����
        if (!canCombo || comboStep == 0)
        {
            comboStep = (comboStep % 3) + 1;
            animator.SetTrigger("Attack" + comboStep); // �ִϸ��̼� Ʈ���� ����
            lastAttackTime = Time.time;
            canCombo = false; // �޺� ���� �� �Ͻ������� ��Ȱ��ȭ

            // ��� ���⿡ ����� �⺻ ���� ����
            currentWeapon?.OnAttackEffect(); // ���� Ư���� �߰� ȿ�� ȣ��
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��Ǵ� �޼���: �޺� ���� �ð� ����
    public void EnableCombo()
    {
        canCombo = true; // ���� �޺��� ���� �� �ִ� ���·� ����
        lastAttackTime = Time.time;
        StartCoroutine(ComboResetTimer()); // ���� �ð� �� �޺��� �ڵ����� �ʱ�ȭ
    }

    // ���� �ð��� ������ �޺� �ʱ�ȭ
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(comboWindow);

        // comboWindow �ð� ���� �߰� �Է��� ������ �޺� �ʱ�ȭ
        if (Time.time - lastAttackTime >= comboWindow)
        {
            ResetCombo();
        }
    }

    public void ResetCombo()
    {
        comboStep = 0;
        canCombo = false;
    }

    public abstract void SkillAttack(int skillIndex); // ���� ĳ���Ͱ� ����
}
