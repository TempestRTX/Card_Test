using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICard : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image cardImage;
    public bool isSelected = false;
    public string cardCode;

    [SerializeField] private GameObject HighlightObject;


    private EventManager eventManager;
    private Transform originalParent;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        eventManager = EventManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        HighlightObject.SetActive(isSelected);
        eventManager.TriggerEvent(appData.OnCardSelected);
        
    }

    public void UpdateParent(Transform parent)
    {
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;
    }
    public void Setup(PlayingCard cardData,Canvas gameScreenCanvas)
    {
        cardImage.sprite = cardData.cardSprite;
        cardCode = cardData.cardCode;
        canvas = gameScreenCanvas;
    }

}