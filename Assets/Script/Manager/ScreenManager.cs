using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] protected appData.AppState ScreenName;
    protected GameManager gameManager;
    protected EventManager eventManager;

    protected virtual void Start()
    {
        
        InitScreen();
    }
    public virtual void OnBackButtonClicked(){
       

    }

    public virtual void InitScreen()
    {
        gameManager = GameManager.Instance;
        eventManager = EventManager.Instance;
    }
}
