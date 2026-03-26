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

    [SerializeField]
    private AudioClip gameMusic;

    private bool preview = true;

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
        AudioManager.Instance.PlayMusic(gameMusic, 0.4f);
        cam = GetComponent<Camera>();
        StartCoroutine(PreviewLevel(true));
    }

    private IEnumerator PreviewLevel(bool callSetup = false)
    {
        LaunchProyectile.canThrowArm = false;
        preview = true;

        yield return new WaitForSeconds(4f);

        if (callSetup) GameManager.Setup(); 

        preview = false;

        LaunchProyectile.canThrowArm = true;
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

        if (trackedProyectile == null && Input.GetKeyDown(KeyCode.C)) StartCoroutine(PreviewLevel());

        if (trackedProyectile != null)
        {
            newPos.y = Mathf.Clamp(trackedProyectile.transform.position.y, catapultPosition.position.y, castlePosition.position.y + 45);
            newPos.x = Mathf.Clamp(trackedProyectile.transform.position.x, catapultPosition.position.x, castlePosition.position.x);

            float speed = trackedRb.linearVelocity.magnitude;
            float targetFOV = Mathf.Clamp(60f + speed / 2f, 60f, 90f);

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 5f);
        }
        else if(!preview)
        {
            trackedRb = null;
            newPos = new Vector3(catapultPosition.position.x, catapultPosition.position.y, transform.position.z);
            smoothSpeed = 2f;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, Time.deltaTime * 2f);
        }

        if(preview) newPos = castlePosition.position;

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, newPos.x, Time.deltaTime * smoothSpeed),
            Mathf.Lerp(transform.position.y, newPos.y, Time.deltaTime * smoothSpeed),
            transform.position.z
        );
    }
}