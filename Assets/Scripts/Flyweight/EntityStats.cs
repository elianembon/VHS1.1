using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/EntityStats", order = 0)]
public class EntityStats : ScriptableObject
{
    [SerializeField] private EntityStatValues _stats;

    public float MaxLife => _stats.MaxLife;

    public float CurrentLife
    {
        get => _stats.CurrentLife;
        set => _stats.CurrentLife = value;
    }

    public float MovementSpeed
    {
        get => _stats.MovementSpeed;
        set => _stats.MovementSpeed = value;
    }

    public float Damage
    {
        get => _stats.Damage;
        set => _stats.Damage = value;
    }




    [System.Serializable]
    public struct EntityStatValues
    {
        public float MaxLife;
        public float CurrentLife;
        public float MovementSpeed;
        public float Damage;
    }
}
