using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController
{
    public readonly int maxHealt;
    public int health { get; private set; }

    /// <returns>Если при получении урона объект теряет всё HP, return false;</returns>
    public bool GetDamage(int damage)
    {
        health -= damage;//формула резиста дамага

        if (health <= 0)
            return false;

        else
            return true;
    }

    public HealthController(int maxHealt)
    {
        this.maxHealt = maxHealt;
        health = maxHealt;
    }
}
