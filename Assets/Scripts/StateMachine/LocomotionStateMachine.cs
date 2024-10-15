using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STM
{
    public class LocomotionStateMachine : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            STM.CharacterBase stmCharacterController = animator.GetComponent<STM.CharacterBase>();
            stmCharacterController.IsPossibleMovement = true;
            stmCharacterController.IsPossibleAttack = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            STM.CharacterBase stmCharacterController = animator.GetComponent<STM.CharacterBase>();
            stmCharacterController.IsPossibleMovement = false;
            stmCharacterController.IsPossibleAttack = false;
        }
    }
}