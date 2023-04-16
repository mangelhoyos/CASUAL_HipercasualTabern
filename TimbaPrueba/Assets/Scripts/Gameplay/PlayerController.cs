using UnityEngine;

/// <summary>
/// Controla el movimiento y el disparo del jugador
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Data driven")]
    [SerializeField] private GameConfigurationSO playerConfiguration;

    [Header("Player movement setup")]
    [SerializeField] private PlateStation[] plateStations;
    private int actualIndex = 1; //Indice uno para que el jugador aparezca en la mesa del medio

    private float actualThrowCounterFireRate;

    private bool isEnabled = true;

    void Update()
    {
        if (!isEnabled)
            return;

        CheckForPlayerMovement();
        CheckForShootingPlate();
    }

    private void CheckForPlayerMovement()
    {
        if (Input.GetKeyDown(playerConfiguration.moveUpInput))
        {
            MovePlayer(-1);
        }
        else if (Input.GetKeyDown(playerConfiguration.moveDownInput))
        {
            MovePlayer(1);
        }
    }
    /// <summary>
    /// Mueve al jugador entre los plate stations
    /// </summary>
    /// <param name="movement">El indice de movimiento (arriba -> -1 o abajo -> 1)</param>
    private void MovePlayer(int movement)
    {
        AudioManager.instance.Play("Move");
        actualIndex = Mathf.Clamp(actualIndex + movement, 0, plateStations.Length - 1);
        transform.position = plateStations[actualIndex].playerMovementPosition.position;
    }

    private void CheckForShootingPlate()
    {
        actualThrowCounterFireRate += Time.deltaTime;

        //Verifica además de la tecla que el tiempo entre disparos se cumpla
        if(Input.GetKeyDown(playerConfiguration.throwPlateInput) && actualThrowCounterFireRate >= playerConfiguration.throwRateFire)
        {
            //Crea un plato y lo envia hacia la derecha, reiniciando el contador
            AudioManager.instance.Play("Throw");
            actualThrowCounterFireRate = 0;
            Rigidbody2D plateRb = Instantiate(playerConfiguration.plateThrowPrefab, plateStations[actualIndex].shootPlatePosition.position, Quaternion.identity);
            plateRb.velocity = Vector2.right * playerConfiguration.throwPlateSpeed;
        }
    }

    /// <summary>
    /// Permite activar o desactivar el controlador del jugador, normalmente usado durante el menú de pausa
    /// </summary>
    /// <param name="isActivated">El nuevo estado que tomará el jugador</param>
    public void ChangePlayerState(bool isActivated)
    {
        isEnabled = isActivated;
    }
}
