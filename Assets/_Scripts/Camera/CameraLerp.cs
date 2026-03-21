using System.Collections;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    private GameObject trackedProyectile;
    [SerializeField]
    private Transform catapultPosition, castlePosition;
    private void OnEnable()
    {
        LaunchProyectile.onProyectileLaunched += AssignTrackedProyectile;
    }

    private void OnDisable()
    {
        LaunchProyectile.onProyectileLaunched -= AssignTrackedProyectile;
    }

    private void AssignTrackedProyectile(GameObject proyectile)
    {
        trackedProyectile = proyectile;
    }

    private void Update()
    {
        if (trackedProyectile != null)
        {
            if(trackedProyectile.transform.position.x < catapultPosition.position.x) return;
            if(trackedProyectile.transform.position.x > castlePosition.position.x) return;
            transform.position = new Vector3(
                trackedProyectile.transform.position.x,
                transform.position.y,
                transform.position.z);
            return;
        }
        else transform.position = new Vector3(
                catapultPosition.transform.position.x,
                transform.position.y,
                transform.position.z);
    }
}
