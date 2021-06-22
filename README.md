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
        //Можно инициализировать методы без входных данных
        //public void OnRecieve();
    }
    //Данные события
    public class MessageData : IEventData
    {
        public string Text;
    }
}
```
