using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Game Event", order = 52)]
public class EventSO : ScriptableObject
{
    // 등록, 해지, 발생
    private List<EventListener> eventListenerList = new List<EventListener>();  // 나를 구독하는 중인 것들

    public void Register(EventListener listener)
    {
        eventListenerList.Add(listener);
    }

    public void UnResister(EventListener listener)
    {
        eventListenerList.Remove(listener);
    }

    public void Occurred(GameObject obj)
    {
        for(int i = 0; i < eventListenerList.Count; i++)
        {
            eventListenerList[i].OnEventOccurs(obj);
        }
    }
}
