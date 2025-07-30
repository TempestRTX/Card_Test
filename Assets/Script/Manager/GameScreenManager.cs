using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenManager : ScreenManager
{
    [SerializeField] private List<PlayingCard> playingCards;
    public override void InitScreen()
    {
        base.InitScreen();
    }

    private void GeneratePlayingCards()
    {
        
    }
}
