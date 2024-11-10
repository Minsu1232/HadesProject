using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttackBase : MonoBehaviour, ICharacterAttack
{
    protected IWeapon currentWeapon;
    protected Animator animator;
    public int comboStep = 0; // 콤보 단계를 관리
    public bool canCombo = false; // 콤보가 가능한지 여부
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
            Debug.Log($"CharacterAttackBase에 {currentWeapon.WeaponName} 무기가 장착되었습니다.");
        }
    }

    private void Update()
    {
        // 콤보 단계 확인을 위한 디버깅 로그
        Debug.Log(comboStep);
    }

    // 공격을 시작하는 메서드: 애니메이션 트리거를 설정하여 공격을 시작
    public virtual void BasicAttack()
    {
       
            
            // 첫 번째 공격인 경우 comboStep을 초기화        
         
            // 콤보에 맞는 콜라이더 활성화
            currentWeapon?.ActivateCollider(comboStep);

            // 애니메이션 트리거 설정
            animator.SetTrigger("Attack");
           

            canCombo = false; // 콤보 중복 방지
            Debug.Log("BasicAttack 시작: " + comboStep);
            
        
    }

    // 애니메이션 이벤트로 호출: 휘두르기 시작 시 콜라이더 활성화
    public void ActivateCollider()
    {
        currentWeapon?.ActivateCollider(comboStep);
        Debug.Log("Collider 활성화됨");
    }

    // 애니메이션 이벤트로 호출: 휘두르기 끝 시 콜라이더 비활성화
    public void DeactivateCollider()
    {
        currentWeapon?.DeactivateCollider();
        Debug.Log("Collider 비활성화됨");
    }   

    public virtual void SkillAttack(int skillIndex)
    {
        throw new System.NotImplementedException();
    }
    public void AttackFinished(int step)
    {
        comboStep = step;  // 전달받은 step 값을 comboStep에 할당
       
    }
}
    // 콤보를 초기화
  
   

    

