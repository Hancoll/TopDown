using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : CharacterAnimation
{
    public void OnEscape(Vector2 direction, float escapeDuration)
    {
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetFloat("escapeAnimationSpeed", 1 / escapeDuration);
        animator.SetTrigger("escape");
    }
}
