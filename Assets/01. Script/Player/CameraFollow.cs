using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // ���� �÷��̾��� Transform
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // ī�޶��� ��ġ ������

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // �÷��̾��� ��ġ�� �������� ������ ��ġ�� ī�޶� ��ġ ����
        transform.position = playerTransform.position + offset;

        // �׻� �÷��̾ �ٶ󺸵��� ī�޶� ����
        transform.LookAt(playerTransform);
    }
}
