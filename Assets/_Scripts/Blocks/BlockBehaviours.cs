using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public BlockData data;

    private float currentHealth;

    void Start()
    {
        currentHealth = data.resistance;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}