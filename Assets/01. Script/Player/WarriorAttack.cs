

using UnityEngine;

public class WarriorAttack : CharacterAttackBase
{
    public override void BasicAttack()
    {
        Debug.Log("Warrior �⺻ ����!");
        base.BasicAttack();

        // ������ ������ Ư�� ���� �޼��带 ���� ���� ȿ�� ����
        currentWeapon?.SpecialAttack();
    }

    public override void SkillAttack(int skillIndex)
    {
        Debug.Log($"Warrior ��ų {skillIndex} ���!");
        // Warrior�� ��ų ���� (ex: ���� Ư���� �г� ����)
    }
}
