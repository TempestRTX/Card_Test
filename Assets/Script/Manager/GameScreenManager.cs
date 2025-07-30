using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameScreenManager : ScreenManager
{
    [SerializeField] private List<PlayingCard> playingCards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject groupPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private float groupSpacing = 300f;
    [SerializeField] private Canvas gameScreenCanvas;
    [SerializeField] private int maxGroups = 3;
    [SerializeField] private int minGroupSize = 2;
    [SerializeField] private int maxGroupSize = 5;

    private List<GameObject> activeGroups = new List<GameObject>();

    public override void InitScreen()
    {
        base.InitScreen();
        GeneratePlayingCards();
    }

    [ContextMenu("Generate Playing Cards")]
    public void RegenerateDeck()
    {
        // Destroy previous groups
        foreach (var group in activeGroups)
        {
            Destroy(group);
        }
        activeGroups.Clear();

        // Recreate groups
        GeneratePlayingCards();
    }

    private void GeneratePlayingCards()
    {
        List<appData.Card> listOfPlayingCards = gameManager.GetCardData();
        Shuffle(listOfPlayingCards);

        // Determine total usable cards based on max groups
        int maxUsableCards = maxGroups * maxGroupSize;
        int usableCardCount = Mathf.Min(listOfPlayingCards.Count, maxUsableCards);

        listOfPlayingCards = listOfPlayingCards.Take(usableCardCount).ToList();

        int index = 0;
        float padding = 50f;

        RectTransform parentRect = cardParent.GetComponent<RectTransform>();
        float startX = parentRect.rect.xMin + padding;

        for (int groupIndex = 0; groupIndex < maxGroups && index < usableCardCount; groupIndex++)
        {
            GameObject newGroup = Instantiate(groupPrefab, cardParent);
            newGroup.transform.localPosition = new Vector3(startX + groupIndex * groupSpacing, 0f, 0f);
            activeGroups.Add(newGroup);

            int remainingCards = usableCardCount - index;
            int groupSize = Random.Range(minGroupSize, maxGroupSize + 1);
            groupSize = Mathf.Min(groupSize, remainingCards); // Don't go out of bounds

            for (int i = 0; i < groupSize; i++)
            {
                GameObject newCard = Instantiate(cardPrefab);
                newCard.transform.SetParent(newGroup.transform, false);

                var cardData = playingCards.FirstOrDefault(x => x.cardCode == listOfPlayingCards[index].cardName);
                if (cardData != null)
                {
                    UICard card = newCard.GetComponent<UICard>();
                    card.UpdateParent(newGroup.transform);
                    card.Setup(cardData, gameScreenCanvas);
                }
                else
                {
                    Debug.LogWarning("No card found with name: " + listOfPlayingCards[index].cardName);
                }

                index++;
            }
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
