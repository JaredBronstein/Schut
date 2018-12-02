using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnEnter : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(1);
    }
}
