using UnityEngine;

public interface IObserver
{
    void OnNotify(string eventInfo, Stop stop, GameObject stopObject);
}