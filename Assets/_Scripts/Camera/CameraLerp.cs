using System.Collections;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    private GameObject trackedProyectile;
    [SerializeField]
    private Transform catapultPosition, castlePosition;

    [SerializeField]
    private Camera cam;

    private Rigidbody2D trackedRb;

    private void OnEnable()
    {
        LaunchProyectile.onProyectileLaunched += AssignTrackedProyectile;
    }

    private void OnDisable()
    {
        LaunchProyectile.onProyectileLaunched -= AssignTrackedProyectile;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void AssignTrackedProyectile(GameObject proyectile)
    {
        trackedProyectile = proyectile;
        trackedRb = proyectile.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 newPos = transform.position;
        float smoothSpeed = 20f;

        if (trackedProyectile != null)
        {
            newPos.y = Mathf.Clamp(trackedProyectile.transform.position.y, catapultPosition.position.y, castlePosition.position.y + 45);
            newPos.x = Mathf.Clamp(trackedProyectile.transform.position.x, catapultPosition.position.x, castlePosition.position.x);

            float speed = trackedRb.linearVelocity.magnitude;
            float targetFOV = Mathf.Clamp(60f + speed / 2f, 60f, 90f);

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 5f);
        }
        else
        {
            trackedRb = null;
            newPos = new Vector3(catapultPosition.position.x, catapultPosition.position.y, transform.position.z);
            smoothSpeed = 2f;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime * 2f);
        }

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, newPos.x, Time.deltaTime * smoothSpeed),
            Mathf.Lerp(transform.position.y, newPos.y, Time.deltaTime * smoothSpeed),
            transform.position.z
        );
    }
}