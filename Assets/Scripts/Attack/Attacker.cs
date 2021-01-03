using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackLayer { Enemy, Player }

public class Attacker : MonoBehaviour
{
    [SerializeField] private AttackerData attacker;
    [SerializeField] private AttackData attack;
    [SerializeField] private float attackHeight = 1f;//высота стрельбы
    private const string attackPrefabName = "Attack";
    private const string playerAttackLayer = "PlayerAttack";
    private const string enemyAttackLayer = "EnemyAttack";

    //УБРАТЬСЯ В ЭТОМ ГОВНЕ КОГДА-НИБУДЬ
    public void Attack(Vector2 attackDirection, AttackLayer attackLayer)
    {
        int attackCount = attacker.attackCount;
        MultiAttackType multiAttackType = attacker.multiAttackType;

        GameObject attackPrefab = Resources.Load(attackPrefabName) as GameObject;
        GameObject attackShell = null;
        Quaternion quaternion = new Quaternion();
        Vector2 spawnPosition;
        Vector2 startPosition;//позиция атаки до поворота 
        float angle = Vector2.SignedAngle(Vector2.right, attackDirection);//DELETE
        int evenC = 0;//если число атак чётное == 1, если нечётное == 0

        if (attackCount % 2 == 0)
            evenC = 1;

        //матрица
        Vector4 column0 = new Vector4(attackDirection.x, attackDirection.y, 0, 0);
        Vector4 column1 = new Vector4(-attackDirection.y, attackDirection.x, 0, 0);
        Vector4 column2 = new Vector4(0, 0, 1, 0);
        Vector4 column3 = new Vector4(0, 0, 0, 1);

        Matrix4x4 characterMatrix = new Matrix4x4(column0, column1, column2, column3);

        for (int i = 0; i < attackCount; i++)
        {
            if (multiAttackType == MultiAttackType.ForwardParallel)
            {
                quaternion.eulerAngles = new Vector3(0, 0, angle);

                startPosition = new Vector2(1, (i - attackCount / 2 + evenC * 0.5f) * attacker.distanceBetweenParallelAttacks);
                spawnPosition = transform.position + characterMatrix.MultiplyPoint(startPosition);

                attackShell = Instantiate(attackPrefab, spawnPosition, quaternion, transform.parent);
                attackShell.GetComponent<AttackController>().SetAttack(attackDirection, attack);
            }

            else if (multiAttackType == MultiAttackType.ForwardFanShaped)
            {
                startPosition = new Vector2(1, 0);
                spawnPosition = transform.position + characterMatrix.MultiplyPoint(startPosition) + attackHeight * Vector3.up;

                float angleBetweenAttacks = (i - attackCount / 2 + evenC * 0.5f) * attacker.angleBetweenFanShapedAttacks * Mathf.Deg2Rad;

                column0 = new Vector4(Mathf.Cos(angleBetweenAttacks), -Mathf.Sin(angleBetweenAttacks), 0, 0);
                column1 = new Vector4(Mathf.Sin(angleBetweenAttacks), Mathf.Cos(angleBetweenAttacks), 0, 0);
                column2 = new Vector4(0, 0, 1, 0);
                column3 = new Vector4(0, 0, 0, 1);

                Matrix4x4 matrix = new Matrix4x4(column0, column1, column2, column3);

                //DELETE
                float _angle = Vector2.SignedAngle(Vector2.right, characterMatrix.MultiplyPoint(startPosition) + matrix.MultiplyPoint(attackDirection));

                quaternion.eulerAngles = new Vector3(0, 0, _angle);

                attackShell = Instantiate(attackPrefab, spawnPosition, quaternion, transform.parent);
                attackShell.GetComponent<AttackController>().SetAttack(matrix.MultiplyPoint(attackDirection), attack);
            }

            else if (multiAttackType == MultiAttackType.MultiSide)
            {
                startPosition = new Vector2(0, 0);
                spawnPosition = transform.position + characterMatrix.MultiplyPoint(startPosition) + attackHeight * Vector3.up;

                float angleBetweenAttacks = 360 / attackCount * i * Mathf.Deg2Rad;

                column0 = new Vector4(Mathf.Cos(angleBetweenAttacks), -Mathf.Sin(angleBetweenAttacks), 0, 0);
                column1 = new Vector4(Mathf.Sin(angleBetweenAttacks), Mathf.Cos(angleBetweenAttacks), 0, 0);
                column2 = new Vector4(0, 0, 1, 0);
                column3 = new Vector4(0, 0, 0, 1);

                Matrix4x4 matrix = new Matrix4x4(column0, column1, column2, column3);

                //DELETE
                float _angle = Vector2.SignedAngle(Vector2.right, characterMatrix.MultiplyPoint(startPosition) + matrix.MultiplyPoint(attackDirection));

                quaternion.eulerAngles = new Vector3(0, 0, _angle);

                attackShell = Instantiate(attackPrefab, spawnPosition, quaternion, transform.parent);
                attackShell.GetComponent<AttackController>().SetAttack(matrix.MultiplyPoint(attackDirection), attack);
            }

            if (attackLayer == AttackLayer.Player)
                attackShell.layer = LayerMask.NameToLayer(playerAttackLayer);
            else if (attackLayer == AttackLayer.Enemy)
                attackShell.layer = LayerMask.NameToLayer(enemyAttackLayer);
        }
    }
}