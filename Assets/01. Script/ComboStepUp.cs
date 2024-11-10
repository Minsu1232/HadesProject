using UnityEngine;

public class ComboStepUpdater : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var characterAttack = animator.GetComponent<CharacterAttackBase>();
        if (characterAttack != null)
        {
            characterAttack.comboStep++;  // comboStep 증가
            Debug.Log("ComboStepUpdater: comboStep 증가됨 - 현재 comboStep: " + characterAttack.comboStep);
        }
    }
}
