using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stop_Manager : MonoBehaviour, IObserver
{

    public List<StopCards> goalStops, nonGoalStops;


    public GameObject currentStop;
    private int numClosedturns;

    public int numCardsCollected;
    [SerializeField] private int numTotalCards;


    private bool firstRecolorUndone = true;

    private void Start()
    {
        ObserverManager.Instance.AddObserver(this);

        RandomSpawnPlace();
    }

    private void Update()
    {
        if (firstRecolorUndone)
        {
            ObserverManager.Instance.NotifyObserver("Recolor Stops", null, currentStop);
            firstRecolorUndone= false;
        }
    }

    private void RandomSpawnPlace()
    {
        int obj = UnityEngine.Random.Range(0, nonGoalStops.Count);

        currentStop = nonGoalStops[obj].stop;
    }

    private void CheckMove(Stop stop, GameObject stopObject)
    {
        for (int i = 0; i < stop.connectedStops.Count; i++)
        {
            if (currentStop == stop.connectedStops[i])
            {
                currentStop = stopObject;
                ObserverManager.Instance.NotifyObserver("Recolor Stops", null, currentStop);
                //Debug.Log(currentStop.name);
                break;
            }
        }

        for (int i = 0; i < goalStops.Count; i++)
        {
            if (currentStop == goalStops[i].stop)
            {
                numCardsCollected++;
                ShowCard(i);

                if (numCardsCollected >= numTotalCards)
                {
                    Debug.Log("Has ganado");
                    //OpenCanvas(winCanvas);
                }
            }
        }
    }

    private void ShowCard(int index)
    {
        //OpenCanvas(canvasCard)
    }

    private void OpenCanvas(GameObject canvas)
    {
        //Opens the designed canvas
    }

    public void OnNotify(string eventInfo, Stop stop, GameObject stopObject)
    {
        if (eventInfo == "Check Movement")
        {
            CheckMove(stop, stopObject);
        }
    }
}

[Serializable]
public class StopCards
{
    public GameObject stop;

    public Image card;
    public string descriptionText;
}
