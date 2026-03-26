using System.Collections;
using UnityEngine;

public class AIArtillery : MonoBehaviour
{
    public Transform catapultArm;
    public Transform proyectileOrigin;
    public GameObject proyectilePrefab;
    public Transform target;

    private AIDifficulty difficulty;

    public float minForce = 300f;
    public float maxForce = 800f;

    public float fireDelay = 2f;

    private float lastForce = 500f;
    private float adjustment = 50f;

    public void SetDifficulty(AIDifficulty diff)
    {
        difficulty = diff;
    }

    void OnEnable()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            AimAtTarget();
            Fire();

            yield return new WaitForSeconds(GetDelay());
        }
    }

    void AimAtTarget()
    {
        Vector2 dir = (target.position - catapultArm.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        catapultArm.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Fire()
    {
        GameObject proj = Instantiate(proyectilePrefab, proyectileOrigin.position, proyectileOrigin.rotation);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        float force = GetForce();

        rb.AddForce(proyectileOrigin.up * force);
    }

    float GetForce()
    {
        if (difficulty == AIDifficulty.Easy)
            return Random.Range(minForce, maxForce);

        float newForce = lastForce + Random.Range(-adjustment, adjustment);
        lastForce = newForce;

        return newForce;
    }

    float GetDelay()
    {
        switch (difficulty)
        {
            case AIDifficulty.Easy: return fireDelay * 2f;
            case AIDifficulty.Medium: return fireDelay;
            case AIDifficulty.Hard: return fireDelay * 0.5f;
        }

        return fireDelay;
    }
}