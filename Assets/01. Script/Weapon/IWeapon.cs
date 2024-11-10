using UnityEngine;

public interface IWeapon
{
    string WeaponName { get; }
    
    string AnimationName { get; }
    string AnimationName1 { get; }
    string AnimationName2 { get; }
    string AnimationName3 { get; }
    void InitializeWeapon(Animator animator);
    int GetDamage(int _baseDamage,int comboStep);
    GameObject WeaponLoad(Transform parentTransform);
    void DeactivateCollider();

    void ActivateCollider(int combostep);
    void ApplyAnimatorOverride(Animator animator); // 애니메이션 오버라이드 적용
    void OnAttackEffect(); // 무기 특유의 추가 효과 (예: 출혈, 경직 등)
    void SpecialAttack(); // 특수 스킬 공격

    int baseDamage {  get;}


}