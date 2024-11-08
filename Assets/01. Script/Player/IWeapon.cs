using UnityEngine;

public interface IWeapon
{
    string WeaponName { get; }
    void InitializeWeapon(Animator animator);
    void ApplyAnimatorOverride(Animator animator); // 애니메이션 오버라이드 적용
    void OnAttackEffect(); // 무기 특유의 추가 효과 (예: 출혈, 경직 등)
    void SpecialAttack(); // 특수 스킬 공격
}