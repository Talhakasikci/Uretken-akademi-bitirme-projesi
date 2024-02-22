using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI countDown, TimeScore, bestScore, lapnum, finishcd, finalMessage;
    private float Count = 3f;
    private float countForGO = 2f;
    public bool isGameStart = false;
    public float Timer = 0f;
    public float finalTime;
    private float CountForFinish = 3f;
    public GameObject panel;
    public Button resume;
    public ParkingControl isParkingOk;
    public CarController carController;
    public AudioSource win;

    private string BestParking1 = "bestPark1";
    private string BestParking2 = "bestPark2";
    private string Besttrack1 = "besttrack1";
    private string Besttrack2 = "besttrack2";
    private bool isBestTime = false;




    // Start is called before the first frame update
    void Start()
    {
        finalTime = 0;
        //En iyi süre için
        string sahneAdi = SceneManager.GetActiveScene().name;
        if (sahneAdi == "Parking1"||sahneAdi == "Parking2")
        {
            
            showParkingTime();
        }else if(sahneAdi == "track1"||sahneAdi == "track2")
        {
            
            showtrackTime();
        }

      //PlayerPrefs.DeleteAll();




        panel.SetActive(false);
        carController = FindObjectOfType<CarController>(); // CarController bileþenini bulur
        if (carController == null)
        {
            Debug.LogError("CarController component not found!");
        }
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        bool isParkOK = isParkingOk.isParkOK;
        
        if(isParkOK == true)
        {
            Time.timeScale = 0f;
            finalTime = Timer;
            win.Play();
            checkBestTimeParking();
           
            isGameStart = false;
            panel.SetActive(true);
            TimeScore.text = "";
            bestScore.text = "";
            lapnum.text = "";
            resume.gameObject.SetActive(false);
            float seconds = Timer;
            float minutes = Mathf.Floor(seconds / 60);
            float secondsRemaining = seconds % 60;
            if (isBestTime)
            {
                finalMessage.text = "New Best Time: " + minutes.ToString() + ":" + secondsRemaining.ToString("F2");
            }
            else
            {
                finalMessage.text = "Time: " + minutes.ToString() + ":" + secondsRemaining.ToString("F2");
            }

           
            
        }
        


        string sahneAdi = SceneManager.GetActiveScene().name;
        int lap = carController.lapControl;

        
        if (lap <= 2)
        {
            finishcd.text = "";
            lapnum.text =lap.ToString();
        }else if (lap >= 3)
        {
            
            CountForFinish -= Time.deltaTime;
            if(CountForFinish > 0)
            {
                if(sahneAdi == "track1"||sahneAdi == "track2")
                {
                    
                    win.Play();
                    finishcd.text = "Finish!!";
                    finalTime = Timer;
                    checkBestTimetrack();
                    
                }
                else
                {
                    finishcd.text = "You lost!";
                }
                
                
            }
            if (CountForFinish <= 0)
            {
                isGameStart = false;
                
                TimeScore.text = "";
                bestScore.text = "";
                lapnum.text = "";

                if (sahneAdi == "track1"||sahneAdi=="track2")
                {
                    Time.timeScale = 0f;
                    float seconds = Timer;
                    float minutes = Mathf.Floor(seconds / 60);
                    float secondsRemaining = seconds % 60;
                    if (isBestTime)
                    {
                        finalMessage.text = "New Best Time: " + minutes.ToString() + ":" + secondsRemaining.ToString("F2");
                    }
                    else
                    {
                        finalMessage.text = "Time: " + minutes.ToString() + ":" + secondsRemaining.ToString("F2");
                    }
                    
                }
                else
                {
                    finalMessage.text = "Try Again";
                }
                
                


                panel.SetActive(true);
                resume.gameObject.SetActive(false);
                finishcd.text = "";
            }
          
            

        }

        

        if (Count >=1&& isGameStart ==false)
        {
            //geri sayým
            countDown.text = ((int)Count).ToString();


            Count -=Time.deltaTime;
            if(Count < 1)
            {
                isGameStart = true; 
                countDown.text = "GO!";
                
                
                
                //geri sayim bitti

            }
        }
        if(isGameStart == true)
        {
            countForGO -= Time.deltaTime;
            if (countForGO <= 0)
            {
                countDown.text = "";
            }
        }
        if(isGameStart == true&&lap<3)
        {
            Timer += Time.deltaTime;
            float seconds = Timer;
            float minutes = Mathf.Floor(seconds / 60);
            float secondsRemaining = seconds % 60;
            
            TimeScore.text = "Time: "+minutes.ToString()+":"+secondsRemaining.ToString("F2");
        }
       
        






    }


    public void checkBestTimeParking()
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        float bestp1 = PlayerPrefs.GetFloat(BestParking1, 1000000000);
        float bestp2 = PlayerPrefs.GetFloat(BestParking2, 1000000000);


        if ((sahneAdi == "Parking1")&&finalTime<bestp1)
        {
            bestp1 = Timer;
            PlayerPrefs.SetFloat(BestParking1, bestp1);
            isBestTime = true;
        }else if((sahneAdi == "Parking2") && finalTime < bestp2)
        {
            isBestTime  =true;
            bestp2 = Timer;
            PlayerPrefs.SetFloat(BestParking2, bestp2);
        }
        
    }

    public void showParkingTime()
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        if(sahneAdi == "Parking1")
        {

            TimerShow(PlayerPrefs.GetFloat(BestParking1));
        }else if(sahneAdi == "Parking2")
        {
            TimerShow(PlayerPrefs.GetFloat(BestParking2));
        }

    }


    public void checkBestTimetrack()
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        float bestt1 = PlayerPrefs.GetFloat(Besttrack1, 1000000000);
        float bestt2 = PlayerPrefs.GetFloat(Besttrack1, 1000000000);


        if ((sahneAdi == "track1") && finalTime < bestt1)
        {
            isBestTime = true;
            bestt1 = Timer;
            PlayerPrefs.SetFloat(Besttrack1, bestt1);
        }
        else if ((sahneAdi == "track2") && finalTime < bestt2)
        {
            isBestTime = true;
            bestt2 = Timer;
            PlayerPrefs.SetFloat(Besttrack2, bestt2);
        }

    }
    public void showtrackTime()
    {
        string sahneAdi = SceneManager.GetActiveScene().name;
        if (sahneAdi == "track1")
        {

            TimerShow(PlayerPrefs.GetFloat(Besttrack1));
        }
        else if (sahneAdi == "track2")
        {
            TimerShow(PlayerPrefs.GetFloat(Besttrack2));
        }

    }



    void TimerShow(float time)
    {

        float seconds = time;
        float minutes = Mathf.Floor(seconds / 60);
        float secondsRemaining = seconds % 60;
        Debug.Log(time);

        bestScore.text = "Best Time: " + minutes.ToString() + ":" + secondsRemaining.ToString("F2");
    }
   

}
