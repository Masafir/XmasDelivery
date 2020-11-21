using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class restartScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void RestartGame()
    {
        print("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
