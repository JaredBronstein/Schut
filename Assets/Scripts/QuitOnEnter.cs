using UnityEngine;
using System.Collections;

public class QuitOnEnter : StateMachineBehaviour //Quits game on the mouse entering the object's collider
{
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
