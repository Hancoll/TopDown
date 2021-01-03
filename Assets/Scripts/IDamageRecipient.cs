using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageRecipient
{
    bool GetDamage(int damage);
    /// <summary>
    /// После анимации смерти выполняется этот метод для уничтожения объекта.
    /// </summary>
    void Dispose();
}
