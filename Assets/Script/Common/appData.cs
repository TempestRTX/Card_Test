using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appData : MonoBehaviour
{
    public enum AppState
    {
        SplashScreen,
        GameScene
    }

    public enum UserAction
    {
        Backbutton,
        PlayGame
        
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
  



}
