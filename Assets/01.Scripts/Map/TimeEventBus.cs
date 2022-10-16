using System.Collections.Generic;
using UnityEngine.Events;

namespace Fruit.Map
{
    public class TimeEventBus
    {
        static readonly IDictionary<TimeEventType, UnityEvent> TimeEvents = new Dictionary<TimeEventType, UnityEvent>();

        public static void Subscribe(TimeEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (TimeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.AddListener(listener);
            } else {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                TimeEvents.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(TimeEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (TimeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(TimeEventType eventType)
        {
            UnityEvent thisEvent;

            if (TimeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.Invoke();
            }
        }
    }
}