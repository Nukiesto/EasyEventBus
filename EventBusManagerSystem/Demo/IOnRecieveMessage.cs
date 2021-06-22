namespace GameEngine.EventBusManagerSystem.Demo
{
    public interface IOnRecieveMessage : ISubscriber
    {
        public void OnRecieve(MessageData eventData);
    }
}