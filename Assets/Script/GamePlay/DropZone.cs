using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    EventManager eventManager;

    private void Start()
    {
        eventManager = EventManager.Instance;
        eventManager.Subscribe(appData.OnCardGrouped,DestoryInvalidGroup());
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

    public Action<object> DestoryInvalidGroup()
    {
        if(transform.childCount ==0)
            Destroy(gameObject);
        return null;
    }
}