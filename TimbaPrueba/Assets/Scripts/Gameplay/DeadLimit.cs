using UnityEngine;

/// <summary>
/// Si el jugador dispara a un lugar donde no hay un orco dará con la zona muerta resultando en GameOver
/// </summary>
public class DeadLimit : MonoBehaviour
{
    private GameManager manager;

    private void Awake() => manager = FindObjectOfType<GameManager>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string PLATETAG = "Plate";
        if (collision.CompareTag(PLATETAG))
        {
            Destroy(collision.gameObject);
            manager.GameOver();
        }
    }
}
