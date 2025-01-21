using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Player playerNight;
    public TuberiaSpawner tuberiaSpawner;
    private int vidas = 0;
    private bool night = false;
    public TextMeshProUGUI puntuacionTexto;
    public GameObject plyr;
    public GameObject plyrNight;
    public GameObject background;
    public GameObject backgroundNight;
    public GameObject playBoton;
    public GameObject pauseBoton;
    public GameObject resumeBoton;
    public GameObject closeBoton;
    public GameObject nightBoton;
    public GameObject dayBoton;
    public GameObject gameOver;
    public GameObject getReady;
    private int puntuacion;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
        getReady.SetActive(true);
        gameOver.SetActive(false);
        pauseBoton.SetActive(false);
        resumeBoton.SetActive(false);
        nightBoton.SetActive(true);
        dayBoton.SetActive(false);
        resumeBoton.SetActive(false);
        resumeBoton.SetActive(false);
#if UNITY_STANDALONE || UNITY_EDITOR
        closeBoton.SetActive(true);
#elif UNITY_ANDROID || UNITY_IOS
        closeBoton.SetActive(false);
        
#endif
    }

    public void Play()
    {
        puntuacion = 0;
        puntuacionTexto.text = puntuacion.ToString();

        playBoton.SetActive(false);
        gameOver.SetActive(false);
        pauseBoton.SetActive(true);
        resumeBoton.SetActive(false);
        nightBoton.SetActive(false);
        dayBoton.SetActive(false);
        GetReady();

        if (NightActive())
        {
            plyr.GetComponent<CircleCollider2D>().enabled = false;
            plyrNight.GetComponent<CircleCollider2D>().enabled = true;
        }
        else
        {
            plyr.GetComponent<CircleCollider2D>().enabled = true;
            plyrNight.GetComponent<CircleCollider2D>().enabled = false;
        }

        Time.timeScale = 1f;
        player.enabled = true;
        playerNight.enabled = true;

        Tuberias[] tuberias = FindObjectsOfType<Tuberias>();
        foreach (var tuberia in tuberias)
        {
            tuberia.gameObject.SetActive(false);
        }

        tuberiaSpawner.RestartSpawner();
    }

    public bool NightActive()
    {
        if (night == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void NightMode()
    {
        night = true;
        plyrNight.transform.position =  new Vector3(plyr.transform.position.x, plyr.transform.position.y, 0);
        plyr.transform.position = new Vector3(0, 0, 1);
        backgroundNight.SetActive(true);
        background.SetActive(false);
        nightBoton.SetActive(false);
        dayBoton.SetActive(true);
    }

    public void DayMode()
    {
        night = false;
        plyr.transform.position = new Vector3(plyrNight.transform.position.x, plyrNight.transform.position.y, 0);
        plyrNight.transform.position = new Vector3(0, 0, 1);        
        backgroundNight.SetActive(false);
        background.SetActive(true); 
        nightBoton.SetActive(true);
        dayBoton.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseBoton.SetActive(false);
        resumeBoton.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseBoton.SetActive(true);
        resumeBoton.SetActive(false);
    }

    public void Morir()
    {
        if (NightActive())
        {
            dayBoton.SetActive(true);
        }
        else
        {
            nightBoton.SetActive(true);            
        }
        Time.timeScale = 0f;

        playerNight.enabled = false;
        player.enabled = false;   
        
        pauseBoton.SetActive(false);
        resumeBoton.SetActive(false);
    }

    public void CerrarJuego()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public async void GetReady()
    {
        getReady.SetActive(true);
        await Task.Delay(2000);
        getReady.SetActive(false);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playBoton.SetActive(true);
        getReady.SetActive(false);
        vidas++;
        Morir();
        if (vidas == 2)
        {
            AdManager.instance.showAd();
            vidas = 0;
        }
    }

    public void GanarPunto()
    {
        puntuacion++;
        puntuacionTexto.text = puntuacion.ToString();
    }
}