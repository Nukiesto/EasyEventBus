using UnityEngine;

namespace EventBusManagerSystem.Demo
{
    public class Reciever : MonoBehaviour, IOnRecieveMessage
    {
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
    }
}