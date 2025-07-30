using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
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
    LoadDataFromLocalFile();
  }

  public List<appData.Card> GetCardData()
  {
    return cardDatas;
  }

  private void SetDeviceOrientation()
  {
    Screen.orientation = deviceOrientation;
  }


  #region  CardData

  public void LoadDataFromLocalFile()
  {
    StartCoroutine(LoadJsonFromStreamingAssets());
  }
  
  private IEnumerator LoadJsonFromStreamingAssets()
  {
    string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);

#if UNITY_ANDROID && !UNITY_EDITOR
        string uri = fullPath; // already has jar:file:// prefix in Android
#else
    string uri = "file://" + fullPath;
#endif

    UnityWebRequest request = UnityWebRequest.Get(uri);
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
      Debug.LogError("Failed to load JSON: " + request.error + " | Path: " + uri);
    }
    else
    {
      string json = request.downloadHandler.text;

      try
      {
        appData.RootWrapper wrapper = JsonConvert.DeserializeObject<appData.RootWrapper>(json);
        cardDatas = wrapper.data.deck
          .Select(name => new appData.Card { cardName = name })
          .ToList();

        Debug.Log("Loaded card names: " + string.Join(", ", cardDatas));
      }
      catch (System.Exception ex)
      {
        Debug.LogError("JSON Deserialization Error: " + ex.Message);
      }
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
      case appData.UserAction.RestartGame:
        ProcessUserRestartAction(state);
        break;
    }
  }

  private void ProcessUserPlayAction( appData.AppState state)
  {
    if (state==appData.AppState.SplashScreen)
    {
      ChangeAppState(appData.AppState.GameScreen);
    }
   
  }

  private void ProcessUserRestartAction(appData.AppState state)
  {
    ChangeAppState(state);  
  }

  private void ChangeAppState(appData.AppState state)
  {
    SceneManager.LoadScene(state.ToString());
  }
  
  #endregion

}
