using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsForGame : MonoBehaviour
{
    private GameObject panel;
    public GameStart gameStart;

    public AudioSource engine;
    public AudioSource idle,brake,click;
    private float sesSeviyesi;
    public CarController carController;
    private bool isGameCont;
    private bool isBrake = false;
    // Start is called before the first frame update
    public void Start()
    {
        sesSeviyesi = PlayerPrefs.GetFloat("SavedCarVolume");
        engine.volume = sesSeviyesi*2;
        idle.volume = (sesSeviyesi/4);
        brake.volume = (sesSeviyesi/4);
       
        isGameCont = true;

        panel = gameStart.panel;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameCont = false;
            engine.Stop();
            idle.Stop();
            brake.Stop();
            Time.timeScale = 0.0f; 
            panel.SetActive(true);
            float seconds = gameStart.Timer;
            float minutes = Mathf.Floor(seconds / 60);
            float secondsRemaining = seconds % 60;
            gameStart.finalMessage.text = "Time: " + minutes.ToString() + "," + secondsRemaining.ToString("F2");
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isBrake = true;
        }
        else
        {
            isBrake = false;
        }

        float gas = carController.gasInput;
        if (gas > 0 && isGameCont && !isBrake)
        {
            if (!engine.isPlaying) // Motor sesi çalýnmýyorsa
            {
                engine.Play(); // Motor sesini çal
            }
            idle.Stop();
            brake.Stop(); 
        }
        else if (gas <= 0 && isGameCont && !isBrake)
        {
            if (!idle.isPlaying) 
            {
                idle.Play(); // Boþta sesini çal
            }
            engine.Stop();
            brake.Stop(); 
        }
        else if (gas == 0 && isGameCont && isBrake)
        {
            if (!brake.isPlaying) 
            {
                brake.Play(); 
            }
            engine.Stop();
            idle.Stop(); 
        }
    }


    public void playAgainTrack1()
    {
        Time.timeScale = 1.0f;
        SaveAndShowBestTime();
        click.Play();
        SceneManager.LoadScene(1);
    }
    public void playAgainTrack2()
    {
        Time.timeScale = 1.0f;
        SaveAndShowBestTime();
        click.Play();
        SceneManager.LoadScene(2);
    }
    public void playAgainPark1()
    {
        Time.timeScale = 1.0f;
        SaveAndShowBestTime();
        click.Play();
        SceneManager.LoadScene(3);
    }
    public void playAgainPark2()
    {
        Time.timeScale = 1.0f;
        SaveAndShowBestTime();
        click.Play();
        SceneManager.LoadScene(4);
    }

    public void mainMenu()
    {
        SaveAndShowBestTime();
        Time.timeScale = 1.0f;
        click.Play();
        SceneManager.LoadScene(0);
    }
    public void resumeGame()
    {
        SaveAndShowBestTime();
        click.Play();
        isGameCont = true;
        Time.timeScale = 1.0f; 
        panel.SetActive(false); 
    }
    public void SaveAndShowBestTime()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        float bestTime = gameStart.finalTime; 

        if (sceneName == "track1")
        {
            PlayerPrefs.SetFloat("BestTrack1Time", bestTime);
        }
        else if (sceneName == "track2")
        {
            PlayerPrefs.SetFloat("BestTrack2Time", bestTime);
        }
        else if (sceneName == "Parking1")
        {
            PlayerPrefs.SetFloat("BestParking1Time", bestTime);
        }
        else if (sceneName == "Parking2")
        {
            PlayerPrefs.SetFloat("BestParking2Time", bestTime);
        }
    }
}
