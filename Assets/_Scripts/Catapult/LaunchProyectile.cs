using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaunchProyectile : MonoBehaviour
{
    [SerializeField]
    GameObject proyectile,proyectileOrigin,catapultArmRotationAxis;

    [SerializeField]
    private AudioClip swingSFX;

    [SerializeField]
    public AudioClip flySFX;

    [SerializeField]
    private Text remainingProyectilesText;
    private int proyectiles;

    private bool canLaunch = false;
    public static bool canThrowArm = false;

    public static event Action <GameObject> onProyectileLaunched;

  
    private void OnEnable()
    {
        GameManager.onGameStart += Setup;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= Setup;
    }

    private void Setup(int gameState)
    {
        proyectiles = 3;
        if (gameState != 0) return;
        canThrowArm = true;
    }

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
        AudioManager.Instance.PlayEffect(swingSFX,transform.position,1,false,0.9f,1.1f);
        AudioManager.Instance.PlayEffect(flySFX,transform.position,0.3f,false,0.9f,1.1f);
        canLaunch = false;
        GameObject newProjectile = Instantiate(proyectile, proyectileOrigin.transform.position, proyectileOrigin.transform.rotation);
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        ProyectileBehaviour behaviour = newProjectile.GetComponent<ProyectileBehaviour>();
        ProyectileData data = behaviour.proyectileData;

        rb.AddForce(proyectileOrigin.transform.up * data.speed);
        onProyectileLaunched?.Invoke(newProjectile);

        proyectiles--;
        remainingProyectilesText.text = "x " + proyectiles;
        if (proyectiles <= 0)
        {
            canThrowArm = false;
            canLaunch = false;
            StopAllCoroutines();
            StartCoroutine(EndGame()); 
        }
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

    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(9f);
        GameManager.IsOutOfProyectiles = true;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) FireProyectile();
    }

}
