using UnityEngine;

/// <summary>
/// La configuración principal del gameplay basado en un modelo data driven
/// </summary>
[CreateAssetMenu(fileName = "Game configuration profile", menuName = "Game data/Game Config")]
public class GameConfigurationSO : ScriptableObject
{
    [Header("Player")]
    public KeyCode moveUpInput;
    public KeyCode moveDownInput;

    public KeyCode throwPlateInput;
    public float throwPlateSpeed;
    public float throwRateFire;
    public Rigidbody2D plateThrowPrefab;

    [Header("Orc")]
    public int scoreValue;
    public Vector2 minNMaxSpawnRate;
    public float maxOrcPatience;
    public float orcIdleSatisfiedTime;

    [Header("Pause")]
    public KeyCode pauseButton;
}
