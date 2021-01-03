using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void SetAnimatorController(RuntimeAnimatorController animatorController)
    {
        animator.runtimeAnimatorController = animatorController;
    }
}
