using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float inactiveRotationSpeed = 100, activatedRotationSpeed = 300;

    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;

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
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
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
