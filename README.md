# EasyEventBus
Простой в использовании EventBus для Unity

Поддерживается наследования от разных событий

Получатель:
```c#
using UnityEngine;

namespace EventBusManagerSystem.Demo
{
    public class Reciever : MonoBehaviour, IOnRecieveMessage
    {
        //Подписка на события
        private void OnEnable()
        {
            EventBusManager.Subscribe(this);
        }
        //Отписка от событий
        private void OnDisable()
        {
            EventBusManager.UnSubscribe(this);
        }
        //Реализация события
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
```

Отправитель:
```c#
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
        {//Способы включения события
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
```
Данные события:
```c#

namespace EventBusManagerSystem.Demo
{
    //Интерфес события
    public interface IOnRecieveMessage : ISubscriber
    {
        public void OnRecieve(MessageData eventData);
        public void OnRecieveExt(string text);
        public void OnRecieveExt2(string text, string text2);
    }
    //Данные события
    public class MessageData : IEventData
    {
        public string Text;
    }
}
```
