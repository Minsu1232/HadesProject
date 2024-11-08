

using UnityEngine;

public class WarriorAttack : CharacterAttackBase
{
    public override void BasicAttack()
    {
        Debug.Log("Warrior 기본 공격!");
        base.BasicAttack();

        // 장착된 무기의 특수 공격 메서드를 통해 무기 효과 적용
        currentWeapon?.SpecialAttack();
    }

    public override void SkillAttack(int skillIndex)
    {
        Debug.Log($"Warrior 스킬 {skillIndex} 사용!");
        // Warrior의 스킬 로직 (ex: 전사 특유의 분노 증가)
    }
}
