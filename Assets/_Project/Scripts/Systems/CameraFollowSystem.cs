using Leopotam.Ecs;
using UnityEngine;

public sealed class CameraFollowSystem : IEcsRunSystem
{
    private readonly EcsFilter<CameraFollow> _cameras = null;
    private readonly EcsFilter<PlayerTag, Position> _player = null;
    
    public void Run()
    {
        Vector3 playerPos = _player.Get2(0).Value;
        
        foreach (var i in _cameras)
        {
            ref var camera = ref _cameras.Get1(i);
            camera.Transform.position = playerPos + camera.Offset;
        }
    }
}