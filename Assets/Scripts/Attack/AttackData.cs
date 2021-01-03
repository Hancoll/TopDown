using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData")]
public class AttackData : ScriptableObject
{
    public int damage;
    public float velocity;
    public float lifeTime;
    public RuntimeAnimatorController animator;
}
