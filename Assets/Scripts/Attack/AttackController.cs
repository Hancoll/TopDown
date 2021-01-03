using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackMove), typeof(AttackAnimation))]
public class AttackController : MonoBehaviour
{
    private Vector2 moveDirection;
    private float lifeTime;
    private AttackData attack;
    private AttackMove attackMove;
    private AttackAnimation attackAnimation;

    private void Awake()
    {
        attackMove = GetComponent<AttackMove>();
        attackAnimation = GetComponent<AttackAnimation>();
    }

    private void FixedUpdate()
    {
        if (GameManager.GameMode)
        {
            lifeTime = Mathf.Clamp(lifeTime - Time.deltaTime, 0, lifeTime);
            if (lifeTime == 0) Dispose();

            attackMove.Move(moveDirection, attack.velocity);//движение
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageRecipient damaged = collision.gameObject.GetComponent<IDamageRecipient>();

        if (damaged == null || (damaged != null && damaged.GetDamage(1)))
            Dispose();
    }

    public void SetAttack(Vector2 moveDirection, AttackData attack)
    {
        attackAnimation.SetAnimatorController(attack.animator);

        lifeTime = attack.lifeTime;
        this.moveDirection = moveDirection;
        this.attack = attack;
    }

    public void Dispose()
    {
        //анимации всякие

        Destroy(gameObject);
    }
}