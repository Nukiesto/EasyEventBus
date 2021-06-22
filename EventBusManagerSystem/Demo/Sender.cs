using UnityEngine;

namespace EventBusManagerSystem.Demo
{
    public class Sender : MonoBehaviour
    {
        private void Start()
        {
            TestEventSend();
        }
        private void TestEventSend()
        {
            var data = new MessageData() {Text = "Test"};
            EventBusManager.RaiseEvent<IOnRecieveMessage>(data, x => x.OnRecieve(null));
            var data2 = new MessageData() {Text = "Test2"};
            EventBusManager.RaiseEvent<IOnRecieveMessage>(data2, nameof(IOnRecieveMessage.OnRecieve));
            
            EventBusManager.RaiseEvent<IOnRecieveMessage>("Test3", nameof(IOnRecieveMessage.OnRecieveExt));
            
            EventBusManager.RaiseEvent<IOnRecieveMessage>(new object[]
            {
                "Test4",
                "Test4_1"
            }, nameof(IOnRecieveMessage.OnRecieveExt2));
        }
    }
}