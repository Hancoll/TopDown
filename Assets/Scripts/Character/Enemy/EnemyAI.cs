using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Moving,
    Attacking,
}

public class EnemyAI : MonoBehaviour
{
    private EnemyState currentState;
    private PlayerController playerController;

    private void Awake() => playerController = GameManager.PlayerController;

    public EnemyState CalculateMoveDirection(float minAttackDistance, float maxAttackDistance, out Vector2 enemyDirection)
    {
        if (playerController != null)
        {
            Vector2 direction = playerController.transform.position - transform.position;
            EnemyState enemyState = EnemyState.Moving;

            //если враг ещё не начал стрелять, то он подходит к игроку на половину от максимальной дальности
            if ((currentState != EnemyState.Attacking && direction.magnitude > (maxAttackDistance - minAttackDistance) / 2 + minAttackDistance) || direction.magnitude > maxAttackDistance)
                direction = direction.normalized;
            //если дистанция меньше минимальной, враг движится от игрока
            else if (minAttackDistance != 0 && ((currentState != EnemyState.Attacking && direction.magnitude < minAttackDistance + 1) || direction.magnitude < minAttackDistance))
                direction = -direction.normalized;
            else//если враг находиться в оптимальной зоне для стрельбы
            {
                direction = direction.normalized;
                enemyState = EnemyState.Attacking;
            }

            currentState = enemyState;
            enemyDirection = direction;
            return enemyState;
        }

        else
        {
            enemyDirection = Vector2.zero;
            return EnemyState.Idle;
        }
    }
}
