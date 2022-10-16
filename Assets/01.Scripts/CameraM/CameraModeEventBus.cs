using System.Collections.Generic;
using UnityEngine.Events;

namespace Fruit.CameraM
{
    public class CameraModeEventBus
    {
        static readonly IDictionary<CameraModeType, UnityEvent> CameraModeEvents = new Dictionary<CameraModeType, UnityEvent>();

        public static void Subscribe(CameraModeType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (CameraModeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.AddListener(listener);
            } else {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                CameraModeEvents.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(CameraModeType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (CameraModeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(CameraModeType eventType)
        {
            UnityEvent thisEvent;

            if (CameraModeEvents.TryGetValue(eventType, out thisEvent)) {
                thisEvent.Invoke();
            }
        }
    }
}