using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerClass
{
    public void Attack();  // ���� �޼��� ����
    public void TakeDamage(int damage);  // ���� ó�� �޼��� ����
    public void UseSkill();  // ��ų ��� �޼��� ����
    public void Die(); // ���� ó�� �޼���
}
