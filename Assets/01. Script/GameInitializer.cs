using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���� ���۽� �÷��̾� ĳ���� �����͸� ����Ƽ ������ ����Ŭ�� �����ϱ� ���� ��ũ��Ʈ
/// </summary>
public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance { get; private set; } // �̱��� �ν��Ͻ�

    private PlayerClass playerClass;
    [SerializeField] private PlayerClassData testData; // �ν����Ϳ��� �Ҵ��� �׽�Ʈ�� ������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
            InitializePlayer();
        }
        else
        {
            Destroy(gameObject); // �ߺ��� GameInitializer�� ������ �ı�
            return;
        }
    }



    private void InitializePlayer()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        Transform playerTransform = transform;

        // ������ ĳ���Ϳ� ���� PlayerClass �ν��Ͻ� ����
        if (testData != null)
        {
            playerClass = testData.classType switch
            {
                PlayerClassData.ClassType.Warrior => new WarriorClass(testData, rb, playerTransform),
                // PlayerClassData.ClassType.Magician => new MagicianClass(testData, rb, playerTransform),
                _ => throw new System.ArgumentException("Invalid character type selected")
            };

        }

        Debug.Log($"Initialized class: {playerClass._playerClassData.name}");

      
    }
    public PlayerClass GetPlayerClass()
    {
        return playerClass; // �ٸ� ��ũ��Ʈ�� playerClass�� ������ �� �ֵ��� ����
    }
}
