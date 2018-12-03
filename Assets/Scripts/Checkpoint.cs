using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Color inactivatedColor, activatedColor;

    [SerializeField]
    private AudioSource activatedSound;

    private bool isActivated;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isActivated)
        {
            activatedSound.Play();
            PlayerController player = collision.GetComponent<PlayerController>();
            player.SetCurrentCheckpoint(this);
        }
    }
    private void UpdateColor()
    {
        Color color = inactivatedColor;

        if (isActivated)
            color = activatedColor;
        spriteRenderer.color = color;
    }
    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateColor();
    }
}
