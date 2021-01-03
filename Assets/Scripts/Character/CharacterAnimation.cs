using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;
    private void Awake() => animator = GetComponent<Animator>();

    public void SetAnimatorController(RuntimeAnimatorController animatorController) => animator.runtimeAnimatorController = animatorController;

    public void SetDirection(Vector2 direction, bool isMoving)
    {
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", isMoving);
    }

    public void GetDamage() => animator.SetTrigger("getDamage");
    /// <summary>
    /// Вызов анимации, по оканчанию которой персонаж исчезнет.
    /// </summary>
    public void Die() => animator.SetTrigger("die");
}


