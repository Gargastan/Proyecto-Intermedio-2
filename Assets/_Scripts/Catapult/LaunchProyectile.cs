using System;
using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class LaunchProyectile : MonoBehaviour
{
    [SerializeField]
    GameObject proyectile,proyectileOrigin,catapultArmRotationAxis;

    private bool canLaunch = false;
    private bool canThrowArm = true;

    public static event Action <GameObject> onProyectileLaunched;
   
    public void FireProyectile()
    {
        if (canThrowArm)
        {
            canThrowArm = false;
            canLaunch = true;
            Quaternion target = catapultArmRotationAxis.transform.rotation * Quaternion.Euler(0, 0, 180);
            StartCoroutine(RotateOverTime(catapultArmRotationAxis.transform, target, 0.5f));
            return;
        }

        if (!canLaunch) return;
        canLaunch = false;
        GameObject newProjectile = Instantiate(proyectile, proyectileOrigin.transform.position, proyectileOrigin.transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1;
        rb.AddForce(proyectileOrigin.transform.up * 1800);
        onProyectileLaunched?.Invoke(newProjectile);

    }

    private IEnumerator RotateOverTime(Transform obj, Quaternion target, float duration)
    {
        Quaternion startRotation = obj.rotation;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            obj.rotation = Quaternion.Lerp(startRotation, target, t);
            yield return null;
        }

        obj.rotation = target;
        yield return new WaitForSeconds(2f);
        canLaunch = false;

        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            obj.rotation = Quaternion.Lerp(target, startRotation, t);
            yield return null;
        }

        obj.rotation = startRotation;
        yield return new WaitForSeconds(0.5f);
        canThrowArm = true;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) FireProyectile();
    }

}
