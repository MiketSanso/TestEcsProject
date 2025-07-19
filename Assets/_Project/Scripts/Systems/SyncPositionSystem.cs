using Leopotam.Ecs;

public sealed class SyncPositionSystem : IEcsRunSystem {
    private readonly EcsFilter<Position, GameObjectReference> _entities = null;

    public void Run() {
        foreach (var i in _entities) {
            ref var pos = ref _entities.Get1(i);
            ref var gameObjectRef = ref _entities.Get2(i);
            
            if (gameObjectRef.Value != null) {
                gameObjectRef.Value.transform.position = pos.Value;
            }
        }
    }
}