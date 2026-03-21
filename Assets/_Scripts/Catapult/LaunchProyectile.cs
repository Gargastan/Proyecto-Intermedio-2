using System;
using UnityEngine;
using UnityEngine.WSA;

public class LaunchProyectile : MonoBehaviour
{
    [SerializeField]
    GameObject proyectile,proyectileOrigin;

    Vector2 launchDirection;

    public static bool canLaunch = true;

    public static event Action <GameObject> onProyectileLaunched;

    public void FireProyectile()
    {
        if (!canLaunch) return;
        canLaunch = false;
        GameObject newProjectile = Instantiate(proyectile, proyectileOrigin.transform.position, proyectileOrigin.transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        GetMouseAngle();

        rb.gravityScale = 1;
        rb.AddForce(launchDirection * 1800);
        onProyectileLaunched?.Invoke(newProjectile);

    }

    private void GetMouseAngle()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        launchDirection = (cam.ScreenToWorldPoint(Input.mousePosition) - proyectileOrigin.transform.position).normalized;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) FireProyectile();
    }

}
