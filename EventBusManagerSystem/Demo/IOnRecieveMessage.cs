
namespace EventBusManagerSystem.Demo
{
    public interface IOnRecieveMessage : ISubscriber
    {
        public void OnRecieve(MessageData eventData);
        public void OnRecieveExt(string text);
        public void OnRecieveExt2(string text, string text2);
    }
    public class MessageData : IEventData
    {
        public string Text;
    }
}