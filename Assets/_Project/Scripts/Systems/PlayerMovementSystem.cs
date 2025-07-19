using Leopotam.Ecs;
using UnityEngine;

public sealed class PlayerMovementSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTag, Position> _players = null;
    private readonly EcsFilter<ArenaData> _arena = null;

    public void Run()
    {
        float arenaRadius = 10f;
        foreach (var i in _arena)
            arenaRadius = _arena.Get1(i).Radius;

        foreach (var i in _players)
        {
            ref var transform = ref _players.Get2(i);

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 moveVector = new Vector3(horizontal, 0, vertical).normalized * 
                                 _players.GetEntity(i).Get<Player>().MoveSpeed * 
                                 Time.deltaTime;
            
            transform.Value += moveVector;
            
            Vector3 pos = transform.Value;
            pos.y = 0;
            if (pos.magnitude > arenaRadius)
            {
                pos = pos.normalized * arenaRadius;
                transform.Value = new Vector3(pos.x, transform.Value.y, pos.z);
            }
        }
    }
}