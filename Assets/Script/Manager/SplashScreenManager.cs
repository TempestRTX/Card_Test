using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplashScreenManager : ScreenManager
{
   private Coroutine _initcoroutine;
   public override void InitScreen()
   {
       _initcoroutine = StartCoroutine(WaitforGameManager());
   }

   private IEnumerator WaitforGameManager()
   {
      yield return new WaitUntil(() => gameManager.IsInit == true);
   }

}
