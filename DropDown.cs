using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public GameObject b4;
    public GameObject b5;
    
    public void DropDownSample(int index)
    {
        switch(index)
        {
            case 0:
            {
                b1.SetActive(true);
                b2.SetActive(true);
                b3.SetActive(true);
                b4.SetActive(true);
                b5.SetActive(true);
                break;
            }
            case 1:
            {
                b1.SetActive(true);
                b2.SetActive(false);
                b3.SetActive(false);
                b4.SetActive(false);
                b5.SetActive(false);
                break;
            }
            case 2:b1.SetActive(false);b2.SetActive(true);b3.SetActive(true);b4.SetActive(true);b5.SetActive(true);break;       
        }
    }
}
    

