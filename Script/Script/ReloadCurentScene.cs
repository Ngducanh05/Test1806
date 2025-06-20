using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ReloadCurentScene : MonoBehaviour
{

   public void ReloadSceneAnh()
    {
        if (GameController.instance.isGameOver)
        {
            GameController.instance.isGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }
}
