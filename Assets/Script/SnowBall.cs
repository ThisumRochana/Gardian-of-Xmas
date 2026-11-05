using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public int points = 1; // points per goblin

    private void OnCollisionEnter(Collision collision)
    {
        // If snowball hits goblin
        if (collision.gameObject.CompareTag("Goblin"))
        {
            // Award points to Santa
            GameManager.Instance.AddPoint(points);

            // **Play goblin hit SFX**
            AudioManager.Instance.PlayGoblinHit();

            // Drop gift if goblin was carrying
            GoblinAI goblin = collision.gameObject.GetComponent<GoblinAI>();
            if (goblin != null)
            {
                goblin.DropGift(); // <-- updated method
            }

            // Destroy goblin and snowball
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        // Destroy snowball if it hits anything else except Santa
        else if (!collision.gameObject.CompareTag("Santa"))
        {
            Destroy(gameObject);
        }
    }
}
