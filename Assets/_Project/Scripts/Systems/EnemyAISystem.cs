using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;

public sealed class EnemyAISystem : IEcsRunSystem
{
    private readonly EcsFilter<EnemyTag, Position, GameObjectReference> _enemies = null;
    private readonly EcsFilter<PlayerTag, Position> _player = null;

    public void Run()
    {
        Vector3 playerPos = Vector3.zero;
        foreach (var i in _player)
            playerPos = _player.Get2(i).Value;

        List<EcsEntity> enemiesToDestroy = new List<EcsEntity>();

        foreach (var i in _enemies)
        {
            ref var enemy = ref _enemies.GetEntity(i).Get<Enemy>();
            ref var pos = ref _enemies.Get2(i);
            ref var gameObjectRef = ref _enemies.Get3(i);
            
            Vector3 direction = (playerPos - pos.Value).normalized;
            pos.Value += direction * enemy.MoveSpeed * Time.deltaTime;
            
            float distance = Vector3.Distance(pos.Value, playerPos);
            if (distance < 1.5f)
            {
                foreach (var p in _player)
                    _player.GetEntity(p).Get<Player>().Health -= 2;
                
                enemiesToDestroy.Add(_enemies.GetEntity(i));
            }
        }

        foreach (var entity in enemiesToDestroy)
        {
            if (entity.Has<GameObjectReference>())
            {
                GameObject.Destroy(entity.Get<GameObjectReference>().Value);
            }
            
            entity.Destroy();
        }
    }
}