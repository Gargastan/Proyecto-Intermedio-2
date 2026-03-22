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
            newPos.y = Mathf.Clamp(trackedProyectile.transform.position.y, catapultPosition.position.y, castlePosition.position.y + 15);
            newPos.x = Mathf.Clamp(trackedProyectile.transform.position.x, catapultPosition.position.x, castlePosition.position.x);

            Rigidbody2D proyectileRb = trackedProyectile.GetComponent<Rigidbody2D>();

            float speed = trackedRb.linearVelocity.magnitude;
            float targetSize = Mathf.Clamp(5f + speed / 14f, 5f, 8f);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * 5f);
        }
        else
        {
            trackedRb = null;
            newPos = new Vector3(catapultPosition.position.x, catapultPosition.position.y, transform.position.z);
            smoothSpeed = 2f;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f, Time.deltaTime * 2f);
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoothSpeed);

        
    }
}
