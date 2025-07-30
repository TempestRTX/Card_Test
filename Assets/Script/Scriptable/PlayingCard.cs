using UnityEngine;

[CreateAssetMenu(fileName = "New Playing Card", menuName = "Card/Playing Card")]
public class PlayingCard : ScriptableObject
{
  [Header("Card Information")]

  [Tooltip("Name of the card (e.g., Ace of Spades)")]
  [SerializeField] public string cardName;

  [Tooltip("Unique code for this card (e.g., AS for Ace of Spades)")]
  [SerializeField] public string cardCode;

  [Tooltip("Sprite image that represents this card visually")]
  [SerializeField] public Sprite cardSprite;
}
