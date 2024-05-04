using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // so we can use TMP_Text class

public class PlayerUI : MonoBehaviour
{
    private TMP_Text puntosTexto;
    private void Awake()
    {
        puntosTexto = GetComponent<TMP_Text>();
    }

    public void ActualizarPuntos(int puntos)
    {
        puntosTexto.text = "Peces comidos: " + puntos.ToString();
    }
}
