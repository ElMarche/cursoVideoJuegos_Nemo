using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PezIA : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    Vector2 limitesPantalla;
    private int dir;
    [SerializeField] private Transform pezSprite;
    private float tamanio;
    // Start is called before the first frame update
    void Start()
    {
        limitesPantalla = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if(transform.position.x <= limitesPantalla.x / 2)
        {
            dir = 1;
            pezSprite.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        } else
        {
            dir = -1;
            pezSprite.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        //Escalar tamanio en modo aleatorio
        float tamanioAleatorio = Random.Range(0.5f, 2.5f);
        tamanio = tamanioAleatorio;
        transform.localScale = new Vector3(tamanioAleatorio, tamanioAleatorio, tamanioAleatorio);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * dir * Time.deltaTime * speed);
        
        if(transform.position.x <= -limitesPantalla.x -2 || transform.position.x > limitesPantalla.x +2)
        {
            Destroy(gameObject);
        }
    }

    public float GetTamanio()
    {
        return tamanio;
    }
}
