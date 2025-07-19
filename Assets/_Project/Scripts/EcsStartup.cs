using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;

    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        _systems
            .Add(new SyncPositionSystem())  
            .Add(new PlayerMovementSystem())
            .Add(new PlayerShootingSystem())
            .Add(new EnemySpawnSystem())
            .Add(new EnemyAISystem())
            .Add(new CameraFollowSystem())
            .OneFrame<SpawnTimer>()
            .Init();
        
        EcsEntity arena = _world.NewEntity();
        arena.Get<ArenaData>() = new ArenaData { Radius = 100f };
        
        EcsEntity player = _world.NewEntity();
        player.Get<Player>() = new Player { 
            MoveSpeed = 5f, 
            Health = 10,
            FireRate = 0.5f 
        };
        player.Get<PlayerTag>();
        player.Get<Position>() = new Position { Value = Vector3.zero };

        GameObject playerObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        playerObj.name = "Player";
        player.Get<GameObjectReference>() = new GameObjectReference { Value = playerObj };
        
        _world.NewEntity().Get<SpawnTimer>() = new SpawnTimer { Value = 1f };
        
        Camera.main.transform.position = new Vector3(0, 15, 0);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }
        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}