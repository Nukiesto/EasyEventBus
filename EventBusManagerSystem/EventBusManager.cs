using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.EventBusManagerSystem
{
    public static class EventBusManager
    {
        private static readonly Dictionary<Type, List<ISubscriber>> SubsDic = new Dictionary<Type, List<ISubscriber>>();
        
        public static void Subscribe(ISubscriber subscriber)
        {
            var subscribes = subscriber.GetType().GetInterfaces().ToList();
            RemoveISubsriber(subscribes);
            
            foreach (var subscribeType in subscribes)
            {
                if (SubsDic.TryGetValue(subscribeType, out var list))
                    list.Add(subscriber);
                else
                {
                    var listSubribes = new List<ISubscriber> {subscriber};
                    SubsDic.Add(subscribeType, listSubribes);
                }
            }
        }
        public static void UnSubscribe(ISubscriber subscriber)
        {
            var subscribes = subscriber.GetType().GetInterfaces().ToList();
            RemoveISubsriber(subscribes);
            
            foreach (var subscribeType in subscribes)
            {
                if (SubsDic.TryGetValue(subscribeType, out var list))
                    list.Remove(subscriber);
            }
        }

        private static void RemoveISubsriber(List<Type> list)
        {
            //Удаляем ISubscriber, так как подписка на него нежелательна
            foreach (var subscribeType in list)
            {
                if (subscribeType == typeof(ISubscriber))
                {
                    list.Remove(subscribeType);
                    break;
                }
            }
        }
        public static void RaiseEvent<T>(IEventData eventData) where T : ISubscriber
        {
            if (SubsDic.TryGetValue(typeof(T), out var subsObjects))
            {
                var methods = typeof(T).GetMethods();
                //foreach (var methodInfo in methods)
                //    Debug.Log(methodInfo.Name);
                foreach (var subscriber in subsObjects)
                {
                    var sub = (T) subscriber;
                    var data = new object[] {eventData};
                    foreach (var methodInfo in methods)
                        methodInfo.Invoke(sub, data);
                }
            }
        }
    }
}