using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 게임 시작시 플레이어 캐릭터 데이터를 유니티 라이프 사이클과 연동하기 위한 스크립트
/// </summary>
public class GameInitializer : Singleton<GameInitializer>
{

    private PlayerClass playerClass;
    private ICharacterAttack characterAttack;
    private IWeapon currentWeapon;

    [SerializeField] private PlayerClassData testData;
    private Animator animator;

    private void Awake()
    {
        InitializePlayer();
     
    }

    private void InitializePlayer()
    {
        animator = GetComponent<Animator>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        Transform playerTransform = transform;
        characterAttack = GetComponent<ICharacterAttack>();

        playerClass = testData.classType switch
        {
            PlayerClassData.ClassType.Warrior => new WarriorClass(testData, characterAttack, rb, playerTransform, animator),
            _ => throw new System.ArgumentException("Invalid character type selected")
        };

        Debug.Log($"Initialized class: {playerClass._playerClassData.name}");
    }

    // 무기 장착 메서드 간소화
    public void EquipWeapon(IWeapon weapon)
    {
        Debug.Log("장착된 무기: 0");

        // characterAttack 또는 weapon이 null인지 확인
        if (characterAttack == null)
        {
            Debug.LogError("characterAttack이 초기화되지 않았습니다.");
            return;
        }
        if (weapon == null)
        {
            Debug.LogError("전달된 weapon이 null입니다.");
            return;
        }

        Debug.Log($"장착된 무기: {weapon.GetType().Name} 초기화 시작");

        // 기존 무기 제거
        if (currentWeapon != null && currentWeapon is Component currentWeaponComponent)
        {
            Destroy(currentWeaponComponent);
            Debug.Log("기존 무기 제거 완료");
        }

        // 새로운 무기 장착
        currentWeapon = gameObject.AddComponent(weapon.GetType()) as IWeapon;

        if (currentWeapon != null)
        {
            characterAttack.EquipWeapon(currentWeapon);
            Debug.Log($"장착된 무기: {currentWeapon.WeaponName}");
        }
        else
        {
            Debug.LogError("무기 컴포넌트를 Player에 추가하는 데 실패했습니다.");
        }
    }

    public IWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public PlayerClass GetPlayerClass()
    {
        return playerClass;
    }
}
