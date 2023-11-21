using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILife
{
    float MaxLife { get; }
    float CurrentLife { get; }

    void TakeDamage(float damage);
    void TakeLife(float RegenLife);
    void Die();
}
