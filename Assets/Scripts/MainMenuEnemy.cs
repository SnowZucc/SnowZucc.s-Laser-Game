using UnityEngine;

public class MainMenuEnemy : MonoBehaviour
{
    public float moveSpeed = 1f; // The speed of movement

    // Start is called before the first frame update
    void Start()
    {
        disableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy to the right and slightly down relative to the world
        transform.position += (Vector3.right + Vector3.down * 0.5f) * moveSpeed * Time.deltaTime;
    }

    public void disableRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = true;
        }
        //animator.enabled = true;
    }
}