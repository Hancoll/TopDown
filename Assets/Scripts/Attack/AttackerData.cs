using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MultiAttackType
{
    ForwardParallel,//атаки летят вперёд паралельно друг-другу
    ForwardFanShaped,//атаки летят вперёд вееро-образно
    MultiSide//атаки летят со всех сторон
}

[CreateAssetMenu(fileName = "AttackerData")]
public class AttackerData : ScriptableObject
{
    public int attackCount;
    public MultiAttackType multiAttackType;
    //параметры паралельной атаки:
    public float distanceBetweenParallelAttacks;
    //параметры вееро-образной атаки:
    public float angleBetweenFanShapedAttacks;
}
