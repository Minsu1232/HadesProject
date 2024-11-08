using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���� ���۽� �÷��̾� ĳ���� �����͸� ����Ƽ ������ ����Ŭ�� �����ϱ� ���� ��ũ��Ʈ
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

    // ���� ���� �޼��� ����ȭ
    public void EquipWeapon(IWeapon weapon)
    {
        Debug.Log("������ ����: 0");

        // characterAttack �Ǵ� weapon�� null���� Ȯ��
        if (characterAttack == null)
        {
            Debug.LogError("characterAttack�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }
        if (weapon == null)
        {
            Debug.LogError("���޵� weapon�� null�Դϴ�.");
            return;
        }

        Debug.Log($"������ ����: {weapon.GetType().Name} �ʱ�ȭ ����");

        // ���� ���� ����
        if (currentWeapon != null && currentWeapon is Component currentWeaponComponent)
        {
            Destroy(currentWeaponComponent);
            Debug.Log("���� ���� ���� �Ϸ�");
        }

        // ���ο� ���� ����
        currentWeapon = gameObject.AddComponent(weapon.GetType()) as IWeapon;

        if (currentWeapon != null)
        {
            characterAttack.EquipWeapon(currentWeapon);
            Debug.Log($"������ ����: {currentWeapon.WeaponName}");
        }
        else
        {
            Debug.LogError("���� ������Ʈ�� Player�� �߰��ϴ� �� �����߽��ϴ�.");
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
