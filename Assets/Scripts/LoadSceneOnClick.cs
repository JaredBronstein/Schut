using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour 
{
    /// <summary>
    /// For Start Button on Main Menu, loads the next scene
    /// </summary>
    /// <param name="sceneIndex">
    /// The Level it transitions to
    /// </param>
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
