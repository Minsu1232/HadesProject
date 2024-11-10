using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var characterAttack = animator.GetComponent<CharacterAttackBase>();
        if (characterAttack != null)
        {
            characterAttack.comboStep = 0;  // Attack 상태로 진입할 때 comboStep 초기화
            Debug.Log("Attack 상태 진입 - comboStep이 0으로 초기화됨");
        }
    }
}
