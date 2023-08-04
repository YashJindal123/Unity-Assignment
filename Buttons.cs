using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public GameObject Client1;
    public GameObject Client2;
    public GameObject Client3;
    public GameObject Client4;
    public GameObject Client5;
    public GameObject Start;
    public GameObject Main;
    public GameObject Task2;
    public GameObject img;

    public void Client11()
    {
        Client1.SetActive(true);
    }
    public void Client12()
    {
        Client2.SetActive(true);
    }
    public void Client13()
    {
        Client3.SetActive(true);
    }
    public void Client14()
    {
        Client4.SetActive(true);
    }
    public void Client15()
    {
        Client5.SetActive(true);
    }

    public void cross()
    {
        Client1.SetActive(false);
        Client2.SetActive(false);
        Client3.SetActive(false);
        Client4.SetActive(false);
        Client5.SetActive(false);
    }

    public void Task1Start()
    {
        Main.SetActive(true);
        Start.SetActive(false);
    }

    public void Task2Start()
    {
        Task2.SetActive(true);
        Start.SetActive(false);
        img.SetActive(false);
    }

    
}

