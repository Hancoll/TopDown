using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMove), typeof(Attacker), typeof(PlayerAnimation))]
public class PlayerController : MonoBehaviour, IDamageRecipient
{
    public System.Action<Collider2D> enterThePassage;
    public System.Action death;
    private Vector2Int playerDirection = Vector2Int.down;//направление взгляда игрока
    private Vector2Int moveDirection;//направление движения игрока
    //escape:
    private bool escape;//при true — игрок в данный момент совершает escape
    private float escapeDuration;
    private float timeBeforeEscape;//время до возможности применить escape
    //attack:
    private float timeBeforeAttack;//время до возможности атаковать
    //references:
    [SerializeField] private PlayerData player;
    [SerializeField] private Collider2D physicsCollider;
    private HealthController healthController;
    private CharacterMove playerMove;
    private Attacker attacker;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        healthController = new HealthController(player.maxHealth);
        playerMove = GetComponent<CharacterMove>();
        attacker = GetComponent<Attacker>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        if (GameManager.GameMode)
        {
            #region Movement
            //escape:
            if (escape)
            {
                if (escapeDuration != 0)
                    playerMove.Move(moveDirection, player.escapeVelocity);

                else escape = false; //остановка escape

                escapeDuration = Mathf.Clamp(escapeDuration -= Time.deltaTime, 0, escapeDuration);
            }

            else//бег
            {
                playerMove.Move(moveDirection, player.moveVelocity);

                bool isMoving = false;

                if (moveDirection != Vector2.zero)
                    isMoving = true;

                playerAnimation.SetDirection(playerDirection, isMoving);

                timeBeforeEscape = Mathf.Clamp(timeBeforeEscape -= Time.deltaTime, 0, timeBeforeEscape);//отсчёт времени до возможности совершения следующего escape
            }

            #endregion
            #region Attack
            timeBeforeAttack = Mathf.Clamp(timeBeforeAttack -= Time.deltaTime, 0, timeBeforeAttack);//отсчёт времени до возможности совершения следующей атаки
            #endregion
        }
    }

    #region Movement
    /// <param name="moveDirection">Направление движения игрока.</param>
    public void SetMoveDirection(Vector2Int moveDirection)
    {
        if (!escape)
            this.moveDirection = moveDirection;
    }

    /// <param name="characterDirection">Направление взгляда персонажа.</param>
    public void SetCharacterDirection(Vector2Int characterDirection) => this.playerDirection = characterDirection;

    public void EscapeAbility()
    {
        if (!escape && timeBeforeEscape == 0)
        {
            escape = true;

            escapeDuration = player.escapeDuration;
            timeBeforeEscape = player.timeBetweenEscape;

            if (moveDirection == Vector2Int.zero)
                moveDirection = playerDirection;

            playerAnimation.OnEscape(moveDirection, player.escapeDuration);
        }
    }
    #endregion

    public void Attack(Vector2 attackDirection)
    {
        if (!escape && timeBeforeAttack == 0)
        {
            attacker.Attack(attackDirection, AttackLayer.Player);

            timeBeforeAttack = player.attackRate;
        }
    }

    public bool GetDamage(int damage)
    {
        if (!escape)
        {
            if (healthController.GetDamage(damage))//если игрок получил урон и не умер, воспроизводится анимация получения дамага
                playerAnimation.GetDamage();

            else//СМЕРТЬ ПЕРСОНАЖА
            {
                playerAnimation.Die();
                death?.Invoke();//оповещение о смерти персонажа
            }

            return true;
        }

        else return false;
    }

    /// <summary>
    /// Метод вызывается при конце анимации смерти.
    /// </summary>
    public void Dispose()
    {

    }
}