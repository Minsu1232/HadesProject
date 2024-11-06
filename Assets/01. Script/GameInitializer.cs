using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 게임 시작시 플레이어 캐릭터 데이터를 유니티 라이프 사이클과 연동하기 위한 스크립트
/// </summary>
public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance { get; private set; } // 싱글턴 인스턴스

    private PlayerClass playerClass;
    [SerializeField] private PlayerClassData testData; // 인스펙터에서 할당할 테스트용 데이터

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
            InitializePlayer();
        }
        else
        {
            Destroy(gameObject); // 중복된 GameInitializer가 있으면 파괴
            return;
        }
    }



    private void InitializePlayer()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        Transform playerTransform = transform;

        // 선택한 캐릭터에 따라 PlayerClass 인스턴스 생성
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
        return playerClass; // 다른 스크립트가 playerClass에 접근할 수 있도록 제공
    }
}
