using System;
using System.Collections.Generic;
public static class EBus
{
    private static readonly Dictionary<string, List<object>> _subscribers = new Dictionary<string, List<object>>();

    public static void Subscribe(string key, Action e)
    {
        if (_subscribers.ContainsKey(key))
            _subscribers[key].Add(e);
        else _subscribers[key] = new List<object> { e };
    }

    public static void Subscribe<T>(string key, Action<T> e)
    {
        if (_subscribers.ContainsKey(key))
            _subscribers[key].Add(e);
        else _subscribers[key] = new List<object> { e };
    }

    public static void UnSubscribe(string key, Action e)
    {
        if (_subscribers.ContainsKey(key))
        {
            _subscribers[key].Remove(e);
            if (_subscribers[key].Count == 0)
                _subscribers.Remove(key);
        }
    }

    public static void UnSubscribe<T>(string key, Action<T> e)
    {
        if (_subscribers.ContainsKey(key))
        {
            _subscribers[key].Remove(e);
            if (_subscribers[key].Count == 0)
                _subscribers.Remove(key);
        }
    }

    public static void Invoke(string key)
    {
        if (_subscribers.ContainsKey(key))
            if (_subscribers[key].Count > 0)
                for (int i = _subscribers[key].Count - 1; i >= 0; i--)
                    (_subscribers[key][i] as Action)?.Invoke();
    }

    public static void Invoke<T>(string key, T arg)
    {
        if (_subscribers.ContainsKey(key))
            if (_subscribers[key].Count > 0)
                for (int i = _subscribers[key].Count - 1; i >= 0; i--)
                    (_subscribers[key][i] as Action<T>)?.Invoke(arg);
    }

    public static bool HasKey(string key) => _subscribers.ContainsKey(key);
}