using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }
    private Estados estados; // Enum class defined at the bottom of this file.
    [SerializeField] private GameObject perdistePanel;
    [SerializeField] private GameObject ganastePanel;
    [SerializeField] private GameObject menuPrincipalPanel;

    //Singleton!!
    private void Awake()
    {
        if (Instancia != null) Destroy(gameObject);
        else Instancia= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To start the game pressing "C" key
        if(this.estados == Estados.MenuPrincipal)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                ActualizarEstados(Estados.Jugando);
            }
        }
        // If you Won or lose and you want to return to main menu
        if(this.estados == Estados.JuegoTerminado || this.estados == Estados.JuegoGanado)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                ActualizarEstados(Estados.MenuPrincipal);
            }
        }
    }

    // function to check gamestates
    public void ActualizarEstados(Estados nuevoEstado)
    {
        this.estados= nuevoEstado;
        switch(estados)
        {
            case Estados.Jugando:
                menuPrincipalPanel.SetActive(false);
                break;
            case Estados.JuegoTerminado:
                //Debug.Log("Perdiste el Juego");
                perdistePanel.SetActive(true); //activates panel
                break;
            case Estados.JuegoGanado:
                //Debug.Log("Ganaste el Juego");
                ganastePanel.SetActive(true); //activates panel
                break;
            case Estados.MenuPrincipal:
                perdistePanel.SetActive(false);
                ganastePanel.SetActive(false);
                menuPrincipalPanel.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            default: 
                break;
        }
    }

    public Estados getEstados()
    {
        return estados;
    }
}

// enum class to manage the game status
public enum Estados
{
    MenuPrincipal,
    Jugando,
    JuegoTerminado,
    JuegoGanado
}
