using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stop_Parameters : MonoBehaviour, IPointerClickHandler, IObserver
{
    [SerializeField] private Stop stop;

    [SerializeField] private GameObject stopObject;
    [SerializeField] private Image image;
    [SerializeField] private Color colorSelected;


    private void Start()
    {
        ObserverManager.Instance.AddObserver(this);
        
        stopObject = this.gameObject;
        image = GetComponent<Image>();
    }

    private void Recolor(GameObject stopObject)
    {
        if (stopObject == this.gameObject)
        {
            image.color = colorSelected;
        }

        else
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ObserverManager.Instance.NotifyObserver("Check Movement", stop, this.gameObject);
    }

    public void OnNotify(string eventInfo, Stop stop, GameObject stopObject)
    {
        if (eventInfo == "Recolor Stops")
        {
            Recolor(stopObject);
        }
    }
}

[Serializable]
public class Stop
{
    public GameObject stop;

    public List<GameObject> connectedStops;
}
