using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_Manager : MonoBehaviour, IObserver
{
    public GameObject currentStop;

    private bool firstRecolorUndone = true;

    private void Start()
    {
        ObserverManager.Instance.AddObserver(this);
        Debug.Log(currentStop.name);
    }

    private void Update()
    {
        if (firstRecolorUndone)
        {
            ObserverManager.Instance.NotifyObserver("Recolor Stops", null, currentStop);
            firstRecolorUndone= false;
        }
    }

    private void CheckMove(Stop stop, GameObject stopObject)
    {
        for (int i = 0; i < stop.connectedStops.Count; i++)
        {
            if (currentStop == stop.connectedStops[i])
            {
                currentStop = stopObject;
                ObserverManager.Instance.NotifyObserver("Recolor Stops", null, currentStop);
                Debug.Log(currentStop.name);
                return;
            }
        }
    }

    public void OnNotify(string eventInfo, Stop stop, GameObject stopObject)
    {
        if (eventInfo == "Check Movement")
        {
            CheckMove(stop, stopObject);
        }
    }
}