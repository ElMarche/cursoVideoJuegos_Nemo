using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float velocidad = 4;
    //SerializeField permite actuar desde el editor
    [SerializeField] private Transform pezSprite;
    private int pecesComidos = 0;
    [SerializeField] private PlayerUI playerUI;
    private float tamanio;
    // Start is called before the first frame update
    void Start()
    {
        tamanio = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Desplazamiento
        float inputHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;
        float inputVertical = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;

        transform.position = transform.position + new Vector3(inputHorizontal, inputVertical, 0);
        // Debug.Log(inputHorizontal + " - " + inputVertical);

        //Para no salirse de la pantalla, trabajamos con los limites de la camara
        Vector2 limitesPantalla = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limitesPantalla.x * -1, limitesPantalla.x),
                             Mathf.Clamp(transform.position.y, limitesPantalla.y * -1, limitesPantalla.y), 0);
        // Rotación
        if (inputHorizontal == 0) return;
        if(inputHorizontal < 0)
        {
            pezSprite.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            pezSprite.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pez"))
        {
            //Eliminar al otro pez y sumar punto al jugador
            PezIA pezIA = collision.gameObject.GetComponent<PezIA>();
            if(tamanio >= pezIA.GetTamanio())
            {
                Destroy(collision.gameObject);
                pecesComidos++;
                playerUI.ActualizarPuntos(pecesComidos);
                if (pecesComidos >= 20)
                {
                    GameManager.Instancia.ActualizarEstados(Estados.JuegoGanado);
                    velocidad = 0;
                }
                transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
                tamanio = transform.localScale.x;
            } else
            {
                Destroy(gameObject);
                //Debug.Log("Game OVER");
                GameManager.Instancia.ActualizarEstados(Estados.JuegoTerminado);
            }
            
        }
    }
}
