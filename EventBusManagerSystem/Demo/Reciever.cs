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

        public void OnRecieveExt(string text)
        {
            Debug.Log(text);
        }

        public void OnRecieveExt2(string text, string text2)
        {
            Debug.Log(text + ":" + text2);
        }
    }
}