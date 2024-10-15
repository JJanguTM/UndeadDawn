using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STM
{
    public class PlayerCharacter : CharacterBase
    {
        public static PlayerCharacter Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        public override void Attack()
        {
            if (IsPossibleAttack == false || IsGrounded == false)
                return;

            if (isAttacking)
                return;

            isAttacking = true;
            characterAnimator.SetTrigger("AttackTrigger");


            Collider[] overlapped = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Character"));
            for (int i = 0; i < overlapped.Length; i++)
            {
                Vector3 forward = transform.forward;
                Vector3 direction = (overlapped[i].transform.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(forward, direction);
                float cosAngleThreshold = Mathf.Cos(30f * Mathf.Deg2Rad);
                if (dotProduct >= cosAngleThreshold)
                {
                    CharacterBase character = overlapped[i].GetComponent<CharacterBase>();
                    character.TakeDamage(10f);
                }
            }
        }
    }
}
   