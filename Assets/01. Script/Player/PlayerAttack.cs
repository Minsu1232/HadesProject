using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private CharacterAttackBase characterAttack;

    private void Start()
    {
        // Player ������Ʈ���� WarriorAttack (�Ǵ� �ٸ� ���� Ŭ����) ������Ʈ ��������
        characterAttack = GetComponent<CharacterAttackBase>();

        if (characterAttack == null)
        {
            Debug.LogError("CharacterAttackBase ������Ʈ�� Player�� ������� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // ���콺 ��Ŭ���� �⺻ �������� ó��
        if (Input.GetButtonDown("Fire1"))
        {
            characterAttack?.BasicAttack(); // WarriorAttack�� �⺻ ���� ȣ��
        }

        // ���콺 ��Ŭ���̳� Ű���� ��ư���� ��ų ���� ó��
        if (Input.GetButtonDown("Fire2"))
        {
            characterAttack?.SkillAttack(1); // WarriorAttack�� ��ų 1 ȣ��
        }
    }
}

  
