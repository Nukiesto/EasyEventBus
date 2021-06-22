using UnityEngine;

namespace GameEngine.EventBusManagerSystem.Demo
{
    public class Recieve : MonoBehaviour, IOnRecieveMessage
    {
        private void Start()
        {
            TestEventSave();
        }

        private void OnEnable()
        {
            EventBusManager.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBusManager.UnSubscribe(this);
        }

        public void OnRecieve(MessageData eventData)
        {
            Debug.Log(eventData.Text);
        }
        
        private void TestEventSave()
        {
            var data = new MessageData() {Text = "Test"};
            EventBusManager.RaiseEvent<IOnRecieveMessage>(data);
        }
    }
}