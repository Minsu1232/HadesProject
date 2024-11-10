using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionUI : MonoBehaviour
{
    public GameObject weaponSelectionPanel;
   [SerializeField] GameObject selectWeapon;

    public void ShowWeaponSelection()
    {
        weaponSelectionPanel.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

    public void EquipGreatSword()
    {
        GreatSword greatSword = GameInitializer.Instance.gameObject.AddComponent<GreatSword>();
        if (greatSword == null)
        {
            Debug.LogError("GreatSword ���⸦ �����ϴ� �� �����߽��ϴ�.");
            return;
        }

        GameInitializer.Instance.EquipWeapon(greatSword);
        Debug.Log("������������");
    }
}
