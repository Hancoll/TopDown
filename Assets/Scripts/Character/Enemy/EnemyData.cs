using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : CharacterData
{
    //attack:
    public float minAttackDistance;//если дистанция меньше этой, враг бежит от персонажа
    public float maxAttackDistance;
}
