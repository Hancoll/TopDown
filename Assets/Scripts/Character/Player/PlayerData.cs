using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : CharacterData
{
    //escape:
    public float escapeVelocity;
    public float escapeDuration;//время совершения escape
    public float timeBetweenEscape;//время до совершения нового escape
}
