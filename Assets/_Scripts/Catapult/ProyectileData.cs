using UnityEngine;

[CreateAssetMenu(fileName = "ProyectileData", menuName = "Boomapult/Proyectile Data")]
public class ProyectileData : ScriptableObject
{
    public string proyectileName;
    public Sprite icon;
    public AudioClip hitSFX, deathSFX;

    [Header("Stats")]
    public int cost;

    [Space]
    public float speed;
    public float speedAcceleration;

    [Space]
    public float angularSpeed;
    public float angularAcceleration;

    [Space]
    public float weight;
    public float weightIncrement;
    public float weightIncrementTimer;
    public Vector2 centerOfMass;

    [Space]
    public float fallTimer;

    [Header("Visual")]
    public GameObject prefab;
}