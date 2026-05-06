namespace GameEngine.Specs {
    public interface IPoolable
    {
        bool IsInPool { get; set; }
        void OnSpawn();
        void OnDespawn();
    }
}