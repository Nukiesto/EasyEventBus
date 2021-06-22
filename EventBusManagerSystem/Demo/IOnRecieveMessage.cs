
namespace EventBusManagerSystem.Demo
{
    public interface IOnRecieveMessage : ISubscriber
    {
        public void OnRecieve(MessageData eventData);
    }
    
    public class MessageData : IEventData
    {
        public string Text;
    }
}