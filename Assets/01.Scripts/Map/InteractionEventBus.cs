using System.Collections.Generic;
using UnityEngine.Events;

namespace Fruit.Map
{
    public class InteractionEventBus
    {
        static readonly IDictionary<InteractionEventType, UnityEvent> InteractionEvents = new Dictionary<InteractionEventType, UnityEvent>();

        public static void Subscribe(InteractionEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (InteractionEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.AddListener(listener);
            } else {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                InteractionEvents.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(InteractionEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (InteractionEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(InteractionEventType eventType)
        {
            UnityEvent thisEvent;

            if (InteractionEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.Invoke();
            }
        }
    }
}