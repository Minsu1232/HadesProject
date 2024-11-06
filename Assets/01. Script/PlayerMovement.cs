using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerClass playerClass; // PlayerClass 인스턴스를 참조
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        if(mainCamera == null)
        {
            Debug.LogError("카메라 못찾ㅁ음");
        }
    }

    private void Start()
    { // GameInitializer를 통해 PlayerClass를 가져와 초기화
        playerClass = GameInitializer.Instance.GetPlayerClass();
    }
    // PlayerClass를 주입받아 이동 속도와 대시 동작을 사용할 수 있도록 설정
    public void Initialize(PlayerClass playerClass)
    {
        this.playerClass = playerClass;
    }

    private void Update()
    {
        if (playerClass == null) return;

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Move(direction);
        RotateTowardsMouse();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dashDirection = (transform.forward * direction.z + transform.right * direction.x).normalized * 15f;
            Debug.Log($"Dashing in Direction: {dashDirection}");
            Dash(dashDirection);
        }
    }
    public void Move(Vector3 direction)
    {
        if (playerClass == null) return;

        // PlayerClass의 CurrentSpeed를 참조하여 이동
        Vector3 moveDirection = (transform.forward * direction.z + transform.right * direction.x).normalized;
        playerClass.rb.MovePosition(playerClass.rb.position + moveDirection * playerClass.CurrentSpeed * Time.fixedDeltaTime);
    }

    public void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetDirection = (hitInfo.point - playerClass.playerTransform.position).normalized;
            targetDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            playerClass.playerTransform.rotation = Quaternion.Slerp(playerClass.playerTransform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }

    public void Dash(Vector3 direction)
    {        
        playerClass?.Dash(direction); // Dash 호출을 PlayerClass에 위임
    }
}
