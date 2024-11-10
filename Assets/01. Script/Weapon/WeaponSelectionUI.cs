using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject weaponSelectionPanel;
   [SerializeField] GameObject selectWeapon;

    public void ShowWeaponSelection()
    {
        weaponSelectionPanel.SetActive(true); // 무기 선택 UI 활성화
    }

    public void EquipGreatSword()
    {
        GreatSword greatSword = GameInitializer.Instance.gameObject.AddComponent<GreatSword>();
        if (greatSword == null)
        {
            Debug.LogError("GreatSword 무기를 생성하는 데 실패했습니다.");
            return;
        }

        GameInitializer.Instance.EquipWeapon(greatSword);
        Debug.Log("무기장착시작");
    }
}
