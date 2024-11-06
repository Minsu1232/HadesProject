using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // 따라갈 플레이어의 Transform
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // 카메라의 위치 오프셋

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // 플레이어의 위치를 기준으로 고정된 위치에 카메라 위치 설정
        transform.position = playerTransform.position + offset;

        // 항상 플레이어를 바라보도록 카메라 설정
        transform.LookAt(playerTransform);
    }
}
