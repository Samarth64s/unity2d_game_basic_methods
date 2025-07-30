using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionController : MonoBehaviour
{
    [Header("Score Settings")] //  This appears as a bold label in Inspector
    [Tooltip("Score increases by 1 when player collects a fruit.")] //  Tooltip appears when you hover over the score field
    public int score = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enter");
            AudioManager.Instance.PlaySoundEffect("Game over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            Debug.Log("Collected: " + collision.gameObject.name);

            // Optional: update score
            score++;
            AudioManager.Instance.PlaySoundEffect("Diamond");
            // Destroy the fruit
            Destroy(collision.gameObject);
            // note add box collider to apple and banana and check is trigger
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("stay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Exit");
        }
    }
}
