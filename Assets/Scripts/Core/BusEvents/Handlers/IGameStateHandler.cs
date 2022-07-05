namespace Core.BusEvents.Handlers
{
    public interface IGameStateHandler : IGlobalSubscriber
    {
        void GoToPuzzle();
        void FinishLevel();
    }
}