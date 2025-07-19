using Leopotam.Ecs;
using UnityEngine;

public sealed class EnemySpawnSystem : IEcsRunSystem
{
    private readonly EcsFilter<SpawnTimer> _timers = null;
    private readonly EcsWorld _world = null;
    private readonly float _spawnRadius = 15f;
    
    public void Run()
    {
        foreach (var i in _timers)
        {
            ref var timer = ref _timers.Get1(i);
            timer.Value -= Time.deltaTime;
            
            if (timer.Value <= 0)
            {
                SpawnEnemy();
                timer.Value = Random.Range(1f, 3f);
            }
        }
    }
    
    private void SpawnEnemy()
    {
        EcsEntity enemy = _world.NewEntity();
        enemy.Get<Enemy>() = new Enemy {
            MoveSpeed = 2f,
            AttackCooldown = 1.5f
        };
        enemy.Get<EnemyTag>();
        
        float angle = Random.Range(0, 360);
        Vector3 spawnPos = new Vector3(
            Mathf.Cos(angle) * _spawnRadius,
            0,
            Mathf.Sin(angle) * _spawnRadius
        );
        
        enemy.Get<Position>().Value = spawnPos;
        
        GameObject enemyObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemyObj.transform.position = spawnPos;
        enemy.Get<GameObjectReference>().Value = enemyObj;
    }
}