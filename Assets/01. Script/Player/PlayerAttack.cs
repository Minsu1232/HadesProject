using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private CharacterAttackBase characterAttack;

    private void Start()
    {
        // Player 오브젝트에서 WarriorAttack (또는 다른 직업 클래스) 컴포넌트 가져오기
        characterAttack = GetComponent<CharacterAttackBase>();

        if (characterAttack == null)
        {
            Debug.LogError("CharacterAttackBase 컴포넌트가 Player에 연결되지 않았습니다.");
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // 마우스 좌클릭을 기본 공격으로 처리
        if (Input.GetButtonDown("Fire1"))
        {
            characterAttack?.BasicAttack(); // WarriorAttack의 기본 공격 호출
        }

        // 마우스 우클릭이나 키보드 버튼으로 스킬 공격 처리
        if (Input.GetButtonDown("Fire2"))
        {
            characterAttack?.SkillAttack(1); // WarriorAttack의 스킬 1 호출
        }
    }
}

  
