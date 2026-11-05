using UnityEngine;

public class GoblinAI : MonoBehaviour
{
    public float speed = 3f;

    private Transform targetGift;
    private bool hasGift = false;
    private Vector3 runTarget;

    public Transform leftSpawn;
    public Transform rightSpawn;
    public float runOffset = 5f;

    private GameObject carriedGift;

    void Start()
    {
        // Assign spawns automatically
        leftSpawn = GameObject.Find("LeftSpawn").transform;
        rightSpawn = GameObject.Find("RightSpawn").transform;

        FindNearestGift();
    }

    void Update()
    {
        if (!hasGift)
        {
            if (targetGift == null)
            {
                // If no gift found, try to find again
                FindNearestGift();
                return;
            }

            MoveTowards(targetGift.position);

            if (Vector3.Distance(transform.position, targetGift.position) < 1f)
            {
                StealGift();
            }
        }
        else
        {
            MoveTowards(runTarget);

            // Reached spawn → gift disappears with goblin
            if (Vector3.Distance(transform.position, runTarget) < 0.5f)
            {
                GiftReachedSpawn();
            }
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    void FindNearestGift()
    {
        GameObject[] gifts = GameObject.FindGameObjectsWithTag("Gift");
        float minDist = Mathf.Infinity;
        targetGift = null;

        foreach (GameObject g in gifts)
        {
            float dist = Vector3.Distance(transform.position, g.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                targetGift = g.transform;
            }
        }
    }

    void StealGift()
    {
        if (targetGift == null) return;

        hasGift = true;

        // Lock gift
        targetGift.tag = "Untagged";

        // Attach gift to goblin
        carriedGift = targetGift.gameObject;
        carriedGift.transform.SetParent(transform);
        carriedGift.transform.localPosition = new Vector3(0.2f, 0.1f, 0.2f);

        // **Play Goblin Stole Gift SFX**
        AudioManager.Instance.PlayGoblinStealGift();

        // Choose random escape spawn
        if (Random.value < 0.5f)
            runTarget = leftSpawn.position + Vector3.left * runOffset;
        else
            runTarget = rightSpawn.position + Vector3.right * runOffset;
    }

    // Called when goblin reaches spawn
    void GiftReachedSpawn()
    {
        // Destroy the gift with goblin
        if (carriedGift != null)
            Destroy(carriedGift);

        GameManager.Instance.GiftStolen();

        Destroy(gameObject, 1.5f);
        enabled = false;
    }

    // Called when goblin is hit by snowball
    public void DropGift()
    {
        if (carriedGift != null)
        {
            // Detach gift from goblin
            carriedGift.transform.SetParent(null);

            // Make gift stealable again
            carriedGift.tag = "Gift";

            // Drop slightly above ground
            carriedGift.transform.position = transform.position + Vector3.up * 0.01f;

            // Optional: add small drop effect if Rigidbody exists
            Rigidbody rb = carriedGift.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            }

            // **Play Goblin Hit SFX**
            AudioManager.Instance.PlayGoblinHit();

            // Reset goblin’s carried gift
            carriedGift = null;
            hasGift = false;

            // Allow this gift to be targeted again
            targetGift = null;
        }
    }
}
