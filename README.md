# 🚀 EventBus for Unity

A simple version of EventBus for Unity, written in C#.
Helps separate systems, easily manage events, and improve code readability.

---

## ✨ Features

- ✅ Subscribe and unsubscribe to events (with or without arguments)
```csharp
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
```
- ✅ Invoke events safely
```csharp
    public static void Invoke(string key)
    {
        if (_subscribers.ContainsKey(key))
            if (_subscribers[key].Count > 0)
                for (int i = _subscribers[key].Count - 1; i >= 0; i--)
                    (_subscribers[key][i] as Action)?.Invoke();
    }
```
- ✅ Check if an event exists  
- ✅ No need to instantiate  

---

## 📦 How use

1. Download `EBus.cs`.  
2. Place it in your Unity project (e.g., `Scripts/Utils`).  
3. Start using it immediately.  

---

## 🛠 Usage

### Subscribing to Events

```csharp
// Subscribe to an event without arguments
EBus.Subscribe("GameOver", OnGameOver);

// Subscribe to an event with one argument
EBus.Subscribe<int>("ScoreUpdated", OnScoreUpdated);

void OnGameOver()
{
    Debug.Log("Game over!");
}

void OnScoreUpdated(int score)
{
    Debug.Log($"New score: {score}");
}
```

---

### Invoking Events

```csharp
// Invoke an event without arguments
EBus.Invoke("GameOver");

// Invoke an event with one argument
EBus.Invoke<int>("ScoreUpdated", 100);
```

---

### Unsubscribing from Events

```csharp
EBus.UnSubscribe("GameOver", OnGameOver);
EBus.UnSubscribe<int>("ScoreUpdated", OnScoreUpdated);
```

---

### Checking if an Event Exists

```csharp
if (EBus.HasKey("GameOver"))
{
    Debug.Log("GameOver event has subscribers");
}
```

---

## 📖 API Reference

| Method | Description |
|--------|-------------|
| `Subscribe(string key, Action e)` | Subscribes a callback without arguments. |
| `Subscribe<T>(string key, Action<T> e)` | Subscribes a callback with one argument. |
| `UnSubscribe(string key, Action e)` | Unsubscribes a callback without arguments. |
| `UnSubscribe<T>(string key, Action<T> e)` | Unsubscribes a callback with one argument. |
| `Invoke(string key)` | Invokes all subscribers without arguments. |
| `Invoke<T>(string key, T arg)` | Invokes all subscribers with one argument. |
| `HasKey(string key)` | Checks if the key has any subscribers. |

---

## ⚠ Notes

- Subscribers are stored in a dictionary with event keys as strings.  
- Supports multiple subscribers per event.  
- Removing a subscriber automatically cleans up empty event lists.  
- Iterates through subscribers **in reverse** to avoid issues if a subscriber removes itself during invocation.  

---

## 📜 Licence
This project is posted solely for my personal portfolio. You may use it for personal purposes, but please credit the author when copying/publishing - [MrSix559](https://github.com/MrSix559);
