using System.Collections;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class ProyectileBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canFall = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Fall(1f));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isDestroying = false;
        if (isDestroying) return;

        isDestroying = true;
        StartCoroutine(Death(5f));
    }

    private IEnumerator Death(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(rb.gameObject);
        LaunchProyectile.canLaunch = true;
    }

    private IEnumerator Fall(float timer)
    {
        yield return new WaitForSeconds(timer);
        canFall = true;
    }

    void Update()
    {
        if(canFall) rb.linearVelocityY -= 0.1f;
    }
}
