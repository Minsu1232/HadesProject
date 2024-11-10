using UnityEngine;

public class ComboStepUpdater : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var characterAttack = animator.GetComponent<CharacterAttackBase>();
        if (characterAttack != null)
        {
            characterAttack.comboStep++;  // comboStep ����
            Debug.Log("ComboStepUpdater: comboStep ������ - ���� comboStep: " + characterAttack.comboStep);
        }
    }
}
