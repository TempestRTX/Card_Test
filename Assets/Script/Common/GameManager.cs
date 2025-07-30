using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;


public class GameManager : GenericSingleton<GameManager>
{
  [SerializeField] private string filePath = "card_data.json";
  [SerializeField] private List<appData.Card> cardDatas = new List<appData.Card>();
  [SerializeField] private ScreenOrientation deviceOrientation = ScreenOrientation.LandscapeLeft;
  
  public bool IsInit = false;

  private void Start()
  {
    SetDeviceOrientation();
    LoadDatafromlocalfile();
  }

  private void SetDeviceOrientation()
  {
    Screen.orientation = deviceOrientation;
  }


  #region  CardData

  private void LoadDatafromlocalfile()
  {
    string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);

    if (File.Exists(fullPath))
    {
      string json = File.ReadAllText(fullPath);
      appData.RootWrapper wrapper = JsonConvert.DeserializeObject<appData.RootWrapper>(json);
        
      cardDatas = wrapper.data.deck.Select(name => new appData.Card { cardName = name }).ToList();
      
      Debug.Log("Loaded card names: " + string.Join(", ", cardDatas));
    }
    else
    {
      Debug.LogError("File not found at path: " + fullPath);
    }
    IsInit = true;
  }


  #endregion
  
  
  #region State Manager

  public void OnUserAction(appData.UserAction action, appData.AppState state)
  {
    switch (action)
    {
      case appData.UserAction.Backbutton:
        //Go back to previous screen
      break;
      case appData.UserAction.PlayGame:
        ProcessUserPlayAction(state);
        break;
        
    }
  }

  private void ProcessUserPlayAction( appData.AppState state)
  {
    ChangeAppState(state);
  }

  private void ChangeAppState(appData.AppState state)
  {
    SceneManager.LoadScene(state.ToString());
  }
  
  #endregion

}
