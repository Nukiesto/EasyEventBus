using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventBusManagerSystem
{
    public interface ISubscriber
    {
        
    }
    public interface IEventData
    {
        
    }
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
        public static void RaiseEvent<T>(IEventData eventData = null, Expression<Action<T>> expression = null) where T : ISubscriber
        {
            if (SubsDic.TryGetValue(typeof(T), out var subsObjects))
            {
                if (expression != null && expression.Body is MethodCallExpression member)
                {
                    var method = member.Method;
                    foreach (var subscriber in subsObjects)
                    {
                        var sub = (T) subscriber;
                        var data = new object[] {eventData};
                        method.Invoke(sub, data);
                    }
                }
                else
                {
                    var method = typeof(T).GetMethods();
                    foreach (var subscriber in subsObjects)
                    {
                        var sub = (T) subscriber;
                        var data = new object[] {eventData};
                        foreach (var methodInfo in method)
                            methodInfo.Invoke(sub, data);
                    }
                }
            }
        }
        public static void RaiseEvent<T>(IEventData eventData = null, string methodName = "") where T : ISubscriber
        {
            if (SubsDic.TryGetValue(typeof(T), out var subsObjects))
            {
                if (methodName != "")
                {
                    var method = typeof(T).GetMethods().FirstOrDefault((s)=>s.Name == methodName);
                    if (method != null)
                    {
                        foreach (var subscriber in subsObjects)
                        {
                            var sub = (T) subscriber;
                            var data = new object[] {eventData};
                            method.Invoke(sub, data);
                        }
                    }
                }
                else
                {
                    var method = typeof(T).GetMethods();
                    foreach (var subscriber in subsObjects)
                    {
                        var sub = (T) subscriber;
                        var data = new object[] {eventData};
                        foreach (var methodInfo in method)
                            methodInfo.Invoke(sub, data);
                    }
                }
            }
        }
    }
}