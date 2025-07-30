using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appData : MonoBehaviour
{
    public enum AppState
    {
        SplashScreen,
        GameScreen
    }

    public enum UserAction
    {
        Backbutton,
        PlayGame,
        RestartGame
        
    }
    [System.Serializable]
    public class RootWrapper
    {
        public Deck data;
    }


    [System.Serializable]
    public class Card
    {
        public string cardName;
    }
    
    [System.Serializable]
    public class Deck
    {
        public List<string> deck; 
    }



    #region Events

    public static string OnCardGrouped = "OnCardGrouped";
    public static string OnCardSelected = "OnCardSelected";
    public static string OnGroupDestroyed = "OnGroupDestroyed";

    #endregion
  



}
