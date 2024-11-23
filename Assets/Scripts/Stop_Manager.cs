using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stop_Manager : MonoBehaviour, IObserver
{

    public List<StopCards> goalStops, nonGoalStops;

    public GameObject currentStop;

    [SerializeField] private int numTotalCards;

    private List<GameObject> matchStops;

    [SerializeField] private GameObject canvasCard, imageCard;

    private Image imageCardCanvas;

    [SerializeField] private TMP_Text textDescription;


    private bool firstRecolorUndone = true;

    private void Start()
    {
        ObserverManager.Instance.AddObserver(this);

        canvasCard.SetActive(false);

        imageCardCanvas = imageCard.GetComponent<Image>();

        RandomSpawnPlace();

        matchStops = ChooseRandomGoals(goalStops, numTotalCards);
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

    private List<GameObject> ChooseRandomGoals(List<StopCards> targetList, int numGoals)
    {
        List<GameObject> resultList = new List<GameObject>();

        GameObject newObject = new GameObject();

        for (int i = 0; i < numGoals; i++)
        {
            do
            {
                int index = UnityEngine.Random.Range(0, targetList.Count);

                newObject = targetList[index].stop;

            } while (resultList.Contains(newObject));

            resultList.Add(newObject);
            newObject = null;
        }

        return resultList;
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

        for (int i = 0; i < matchStops.Count; i++)
        {
            if (currentStop == matchStops[i])
            {
                matchStops.Remove(currentStop);

                ShowCard(i);

                if (matchStops.Count <= 0)
                {
                    Debug.Log("Has ganado");
                    //OpenCanvas(winCanvas);
                }
            }
        }
    }

    private void ShowCard(int index)
    {
        OpenCanvas(canvasCard);

        imageCardCanvas = goalStops[index].card;
        textDescription.text = goalStops[index].descriptionText;
    }

    private void OpenCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
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
