using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour //Called from the quit button to exit the game
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
