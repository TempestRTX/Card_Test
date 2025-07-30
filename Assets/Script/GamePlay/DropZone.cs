using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    private EventManager eventManager;

    private void Start()
    {
        eventManager = EventManager.Instance;
        eventManager.Subscribe(appData.OnCardGrouped, OnCardGrouped);
    }

    private void OnDestroy()
    {
        
        if (eventManager != null)
            eventManager.Unsubscribe(appData.OnCardGrouped, OnCardGrouped);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var card = eventData.pointerDrag.GetComponent<UICard>();
        if (card != null)
        {
            card.transform.SetParent(transform);
            card.UpdateParent(transform);
            eventManager.TriggerEvent(appData.OnCardGrouped);
        }
    }

    private void OnCardGrouped(object data)
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}