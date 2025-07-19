using Leopotam.Ecs;
using UnityEngine;

public struct Enemy
{
    public float MoveSpeed;
    public float AttackCooldown;
    public float CurrentCooldown;
}

public struct EnemyTag { }