using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static void RaiseEvent<T>(this MonoBehaviour monoBehaviour, EventHandler<T> eventHandler, T data)
    {
        EventHandler<T> handler = eventHandler;
        if (handler != null)
        {
            handler(monoBehaviour, data);
        }
    }
    public static void RaiseEvent(this MonoBehaviour monoBehaviour, EventHandler eventHandler)
    {
        EventHandler handler = eventHandler;
        if (handler != null)
        {
            handler(monoBehaviour, null);
        }
    }
}
