using UnityEngine;
using System.Collections;

public class QuitOnEnter : StateMachineBehaviour
{
    /// <summary>
    /// Quits game on the mouse entering the object's collider
    /// </summary>
    /// <param name="animator">Animator of the button</param>
    /// <param name="stateInfo">Info on the game state</param>
    /// <param name="layerIndex">Layer</param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Quit();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
