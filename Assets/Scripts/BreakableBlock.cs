using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    /// <summary>
    /// Destroys self after playing an animation when in contact with a bullet
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            anim.SetBool("IsBroken", true);
            boxCollider.enabled = false;
            Invoke("Destroy", 0.1f);
        }
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
