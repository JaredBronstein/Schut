using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour
{
    /// <summary>
    /// Called from the quit button to exit the game
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
