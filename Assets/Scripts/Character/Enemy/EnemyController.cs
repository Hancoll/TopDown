using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMove), typeof(Attacker), typeof(CharacterAnimation))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyController : MonoBehaviour, IDamageRecipient
{
    [SerializeField] private EnemyData enemyData;
    private Vector2 enemyDirection;//направление взгляда врага
    private bool isDead;
    //attack:
    private float timeBeforeAttack;//время до возможности атаковать
                                   //references:
    private HealthController healthController;
    private CharacterMove enemyMove;
    private EnemyAI enemyAI;
    private Attacker attacker;
    private CharacterAnimation enemyAnimation;

    private void Awake()
    {
        healthController = new HealthController(enemyData.maxHealth);
        enemyMove = GetComponent<CharacterMove>();
        enemyAI = GetComponent<EnemyAI>();
        attacker = GetComponent<Attacker>();
        enemyAnimation = GetComponent<CharacterAnimation>();
    }

    private void FixedUpdate()
    {
        if (GameManager.GameMode && !isDead)
        {
            EnemyState enemyState = enemyAI.CalculateMoveDirection(enemyData.minAttackDistance, enemyData.maxAttackDistance, out Vector2 enemyDirection);

            if (enemyState == EnemyState.Attacking)
            {
                Idle(enemyDirection);//вращение за персонажем

                if (timeBeforeAttack == 0)//НАЧАЛО АТАКИ
                {
                    //анимация стрельбы
                    attacker.Attack(enemyDirection, AttackLayer.Enemy);
                    timeBeforeAttack = enemyData.attackRate;
                }
            }

            else if (enemyState == EnemyState.Moving)
                Move(enemyDirection, enemyData.moveVelocity);

            timeBeforeAttack = Mathf.Clamp(timeBeforeAttack - Time.deltaTime, 0, enemyData.attackRate);
        }
    }

    public bool GetDamage(int damage)
    {
        if (!isDead)//если враг жив
        {
            if (healthController.GetDamage(damage))//если враг получил урон и не умер, воспроизводится анимация получения дамага
            {
                enemyAnimation.GetDamage();
                return true;//атака, которая поразила врага, уничтожается 
            }

            else
            {
                isDead = true;//смена состояния персонажа на МЁРТВ
                enemyAnimation.Die();
                return false;//атака, которая поразила врага, идёт дальше и не уничтотжается 
            }
        }

        else return false;//если враг мёртв, то пули при поражении его идут дальше и не уничтожаются
    }

    /// <summary>
    /// Метод вызывается при конце анимации смерти.
    /// </summary>
    public void Dispose() => Destroy(gameObject);

    #region Movement
    private void Move(Vector2 moveDirection, float moveVelocity)
    {
        enemyMove.Move(moveDirection, moveVelocity);
        enemyAnimation.SetDirection(moveDirection, true);
    }

    private void Idle(Vector2 enemyDirection)
    {
        enemyMove.Move(Vector2.zero, 0);
        enemyAnimation.SetDirection(enemyDirection, false);
    }
    #endregion
}