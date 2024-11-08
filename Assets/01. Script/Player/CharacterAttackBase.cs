using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttackBase : MonoBehaviour, ICharacterAttack
{
    protected IWeapon currentWeapon;
    protected Animator animator;
    public int comboStep = 0;
    public bool canCombo = false; // 콤보가 가능한지 여부
    private float comboWindow = 0.5f; // 다음 콤보 입력이 가능한 시간(예: 0.5초)
    protected float lastAttackTime;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GameInitializer.Instance.GetCurrentWeapon(); // 현재 장착된 무기를 가져옴
    }
    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
        if (currentWeapon != null)
        {
            currentWeapon.InitializeWeapon(animator); // 무기 초기화 및 애니메이션 오버라이드
            Debug.Log($"CharacterAttackBase에 {currentWeapon.WeaponName} 무기가 장착되었습니다.");
        }
    }
    public virtual void BasicAttack()
    {
        // 콤보가 가능하거나 첫 번째 공격일 때만 공격을 실행
        if (!canCombo || comboStep == 0)
        {
            comboStep = (comboStep % 3) + 1;
            animator.SetTrigger("Attack" + comboStep); // 애니메이션 트리거 설정
            lastAttackTime = Time.time;
            canCombo = false; // 콤보 실행 후 일시적으로 비활성화

            // 모든 무기에 공통된 기본 공격 실행
            currentWeapon?.OnAttackEffect(); // 무기 특유의 추가 효과 호출
        }
    }

    // 애니메이션 이벤트로 호출되는 메서드: 콤보 유지 시간 설정
    public void EnableCombo()
    {
        canCombo = true; // 다음 콤보를 받을 수 있는 상태로 설정
        lastAttackTime = Time.time;
        StartCoroutine(ComboResetTimer()); // 일정 시간 후 콤보를 자동으로 초기화
    }

    // 일정 시간이 지나면 콤보 초기화
    private IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(comboWindow);

        // comboWindow 시간 내에 추가 입력이 없으면 콤보 초기화
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

    public abstract void SkillAttack(int skillIndex); // 개별 캐릭터가 구현
}
