using System.Collections;
using UnityEngine;

/// <summary>
/// Maneja el estado de los orcos que deben ser alimentados como spawn, emociones y sumar puntos
/// </summary>
public class HungryOrc : MonoBehaviour
{
    private static GameManager manager;

    [Header("Orc setup")]
    [SerializeField] private GameConfigurationSO gameConfig;
    [SerializeField] private GameObject visualObject;

    [Header("Emotion system")]
    [SerializeField] private SpriteRenderer emotionRenderer;
    [SerializeField] private Sprite[] emotionsSprites;

    private float currentWaitingTime = 0;
    private bool isWaiting = false;

    void Awake() => manager = FindObjectOfType<GameManager>();
    private void Start()
    {
        StartCoroutine(RandomSpawnCoroutine());
    }

    void Update()
    {
        if(isWaiting)
        {
            currentWaitingTime += Time.deltaTime;
            CalculateEmotions();
            if(currentWaitingTime >= gameConfig.maxOrcPatience)
            {
                currentWaitingTime = 0;
                manager.GameOver();
            }
        }
    }

    /// <summary>
    /// Detecta si es alimentado con un plato
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string PLATETAG = "Plate";
        if(isWaiting && collision.CompareTag(PLATETAG))
        {
            Destroy(collision.gameObject);
            manager.AddScore(gameConfig.scoreValue);
            StartCoroutine(OrcSatisfiedCoroutine());
        }
    }

    private void CalculateEmotions()
    {
        //Si ha esperado mucho cambiar al emoji de enojado
        if(currentWaitingTime >= gameConfig.maxOrcPatience / 2)
        {
            emotionRenderer.sprite = emotionsSprites[1];
        }
    }

    /// <summary>
    /// Inicializa el orco poniendolo como activo
    /// </summary>
    private void InitializeOrc()
    {
        AudioManager.instance.Play("Appear");
        visualObject.SetActive(true);
        isWaiting = true;
    }

    /// <summary>
    /// Devuelve al orco a un estado inicial y desabilitado
    /// </summary>
    private void ResetOrc()
    {
        visualObject.SetActive(false);
        emotionRenderer.sprite = emotionsSprites[0];
        isWaiting = false;
        currentWaitingTime = 0;
    }

    /// <summary>
    /// Elige un tiempo al azar para que el orco aparezca una vez es llamado
    /// </summary>
    IEnumerator RandomSpawnCoroutine()
    {
        ResetOrc();

        float getRandomTime = Random.Range(gameConfig.minNMaxSpawnRate.x, gameConfig.minNMaxSpawnRate.y);
        float getSecondRandomTime = Random.Range(-1,2);
        yield return new WaitForSeconds(getRandomTime + getSecondRandomTime);

        InitializeOrc();
    }

    IEnumerator OrcSatisfiedCoroutine()
    {
        isWaiting = false;
        emotionRenderer.sprite = emotionsSprites[2];

        yield return new WaitForSeconds(gameConfig.orcIdleSatisfiedTime);

        StartCoroutine(RandomSpawnCoroutine());
    }
}
