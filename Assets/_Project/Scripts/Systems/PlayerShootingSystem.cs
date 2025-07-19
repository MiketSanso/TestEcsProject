using Leopotam.Ecs;
using UnityEngine;

public sealed class PlayerShootingSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTag, Player> _players = null;
    private readonly EcsWorld _world = null;

    public void Run()
    {
        foreach (var i in _players)
        {
            ref var player = ref _players.Get2(i); 
            ref var playerEntity = ref _players.GetEntity(i);
            
            player.FireTimer -= Time.deltaTime;
            
            if (player.FireTimer <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    Vector3 shootDirection = (hit.point - playerEntity.Get<Position>().Value).normalized;
                    shootDirection.y = 0;
                    
                    EcsEntity projectile = _world.NewEntity();
                    projectile.Get<Projectile>() = new Projectile
                    {
                        Speed = 15f,
                        Direction = shootDirection
                    };
                    
                    GameObject projObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    projObj.transform.position = playerEntity.Get<Position>().Value + shootDirection;
                    projObj.transform.localScale = Vector3.one * 0.3f;
                    projectile.Get<Position>().Value = projObj.transform.position;
                    projectile.Get<GameObjectReference>().Value = projObj;
                    
                    player.FireTimer = player.FireRate;
                }
            }
        }
    }
}