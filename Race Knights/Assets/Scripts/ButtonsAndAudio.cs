using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonsAndAudio : MonoBehaviour
{
    public GameObject PanelForRacing;
    public GameObject PanelForParking;
    public GameObject PanelForSettings;
    public AudioSource menuMusic;
    private AudioSource click;
    public Slider menuSlider,carEngineSlider;
    public TextMeshProUGUI volumeAmountText,vACar;
    public float savedVolume,savedCarVolume;
    

    // Start is called before the first frame update
    void Start()
    {
        click = GetComponent<AudioSource>();
        PanelForParking.SetActive(false);
        PanelForRacing.SetActive(false);
        PanelForSettings.SetActive(false);
        
        

        //menu için
        if (PlayerPrefs.HasKey("SavedVolume"))
        {
            savedVolume = PlayerPrefs.GetFloat("SavedVolume");
            menuSlider.value = savedVolume;
            setVolume(savedVolume); // Ses seviyesini ayarla
        }
        else
        {
            savedVolume = menuMusic.volume;
            PlayerPrefs.SetFloat("SavedVolume", savedVolume);
        }

        volumeAmountText.text = ((int)(menuSlider.value * 100)).ToString();
        //araba için
        if (PlayerPrefs.HasKey("SavedCarVolume"))
        {
            savedCarVolume = PlayerPrefs.GetFloat("SavedCarVolume");
            carEngineSlider.value = savedCarVolume;
            setVolumeCar(savedCarVolume); // Ses seviyesini ayarla
        }
        else
        {
            PlayerPrefs.SetFloat("SavedCarVolume", savedVolume);
        }


    }

    public void QuitButton()
    {
        click.Play();
        Application.Quit();
    }

    public void RacingButton()
    {
        click.Play();
        PanelForRacing.SetActive(true);
    }

    public void ParkingButton()
    {
        click.Play();
        PanelForParking.SetActive(true);
    }

    public void settings()
    {
        click.Play();
        PanelForSettings.SetActive(true);
    }

    public void MainMenuButton()
    {
        click.Play();
        PanelForParking.SetActive(false);
        PanelForRacing.SetActive(false);
        PanelForSettings.SetActive(false);
    }

    public void trac1()
    {
        click.Play();
        SceneManager.LoadScene(1);
    }

    public void trac2()
    {
        click.Play();
        SceneManager.LoadScene(2);
    }

    public void park1()
    {
        click.Play();
        SceneManager.LoadScene(3);
    }

    public void park2()
    {
        click.Play();
        SceneManager.LoadScene(4);
    }

    public void setVolume(float volume)
    {
        menuMusic.volume = volume;
        click.volume = volume;
        volumeAmountText.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("SavedVolume", volume);

    }
    public void setVolumeCar(float volume)
    {
        carEngineSlider.value = volume;
        PlayerPrefs.SetFloat("SavedCarVolume", volume);
        vACar.text = ((int)(volume * 100)).ToString();
    }

   


}
