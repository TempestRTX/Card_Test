using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] protected appData.AppState ScreenName;
    protected GameManager gameManager;
    protected EventManager eventManager;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        eventManager = EventManager.Instance;
        InitScreen();
    }
    public virtual void OnBackButtonClicked(){
       

    }

    public virtual void InitScreen()
    {
       
    }
}
