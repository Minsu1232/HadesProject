using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerClass
{
    public void Attack();  // 공격 메서드 정의
    public void TakeDamage(int damage);  // 피해 처리 메서드 정의
    public void UseSkill();  // 스킬 사용 메서드 정의
    public void Die(); // 죽음 처리 메서드
}
