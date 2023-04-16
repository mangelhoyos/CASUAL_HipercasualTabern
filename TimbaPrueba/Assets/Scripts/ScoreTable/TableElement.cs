using TMPro;
using UnityEngine;

/// <summary>
/// Elemento de la tabla que contendrá la información de cada uno de los usuarios con puntuación
/// </summary>
public class TableElement : MonoBehaviour
{
    [Header("Text fields")]
    [SerializeField] private TMP_Text listNumber;
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text points;

    /// <summary>
    /// Establece los datos del elemento en la tabla
    /// </summary>
    public void SetTableElementData(string listNumber, string username, string points)
    {
        this.listNumber.text = listNumber;
        this.username.text = username;
        this.points.text = points;
    }
}
