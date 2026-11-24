/***************************************************************
*file: ContactAOEPhysics.cs
*author: G. Valdiviez
*class: CS 4700 – Game Development
*assignment: Final Program
*date last modified: 11/20/2026
*
*purpose: Visualizes the Contact-AOE projectiles' explosions.
*
****************************************************************/
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionFade : MonoBehaviour
{
    private SpriteRenderer explosionSprite;
    private float duration;
    private float elapsedTime;

    private Color originalColor;

    // function: Initialize
    // Purpose: Sets the explosionLine to the SpriteRenderer parameter, and duration to the fadeDuration parameter.
    //          Also sets originalColor to the SpriteRenderer parameter's colors.
    public void Initialize(SpriteRenderer sprite, float fadeDuration)
    {
        explosionSprite = sprite;
        duration = Mathf.Max(0.01f, fadeDuration);

        originalColor = sprite.color;
    }

    // function: Update
    // Purpose: Called every frame while the script is active
    //          Causes the explosion radius visual to fade based on duration and elapsedTime.
    void Update()
    {
        if (explosionSprite == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float progress = Mathf.Clamp01(elapsedTime / Mathf.Max(0.01f, duration));
        float alpha = 1f - progress;

        Color currentColor = originalColor;
        currentColor.a = originalColor.a * alpha;

        explosionSprite.color = currentColor;

        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
