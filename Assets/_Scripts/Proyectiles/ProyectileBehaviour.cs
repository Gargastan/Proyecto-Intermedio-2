using System.Collections;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
public class ProyectileBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canFall = false;
    private bool isTouching = false;
    //private bool isDiving = false;

    public ProyectileData proyectileData;

    private float gravityMod = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Fall());

        rb.angularVelocity -= proyectileData.angularSpeed;

        if(proyectileData.centerOfMass == Vector2.zero) return;
        rb.centerOfMass = proyectileData.centerOfMass;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouching = true;
        rb.mass += proyectileData.weightIncrement;
        rb.linearDamping += proyectileData.weightIncrement;
        rb.angularDamping += proyectileData.weightIncrement;
        StartCoroutine(Death(5f));
    }
    private void OnCollisionExit(Collision collision)
    {
        isTouching = false ;
    }

    private IEnumerator Death(float timer)
    {
        yield return new WaitForSeconds(timer);
        AudioManager.Instance.PlayEffect(proyectileData.deathSFX);
        Destroy(rb.gameObject);
        LaunchProyectile.canThrowArm = true;
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(proyectileData.fallTimer);
        canFall = true;
        if (proyectileData.weightIncrementTimer > 0) StartCoroutine(Dive());
    }

    private IEnumerator Dive()
    {
        yield return new WaitForSeconds(proyectileData.weightIncrementTimer);
        //isDiving = true;
    }

    void Update()
    {
        if(canFall&&!isTouching) rb.linearVelocityY -= gravityMod + proyectileData.weight/100;
        //if(isDiving)gravityMod += proyectileData.weightIncrement/1000;
        rb.angularVelocity -= proyectileData.angularAcceleration;
        rb.linearVelocityX += proyectileData.speedAcceleration/100;
       
    }
}
