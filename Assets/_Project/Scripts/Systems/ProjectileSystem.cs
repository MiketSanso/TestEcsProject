using Leopotam.Ecs;
using UnityEngine;

public sealed class ProjectileSystem : IEcsRunSystem
{
    private readonly EcsFilter<Projectile, Position> _projectiles = null;
    
    public void Run()
    {
        foreach (var i in _projectiles)
        {
            ref var proj = ref _projectiles.Get1(i);
            ref var pos = ref _projectiles.Get2(i);
            
            pos.Value += proj.Direction * proj.Speed * Time.deltaTime;
            proj.Lifetime -= Time.deltaTime;
            
            if (proj.Lifetime <= 0)
            {
                if (_projectiles.GetEntity(i).Has<GameObjectReference>())
                {
                    GameObject.Destroy(
                        _projectiles.GetEntity(i).Get<GameObjectReference>().Value
                    );
                }
                _projectiles.GetEntity(i).Destroy();
            }
        }
    }
}