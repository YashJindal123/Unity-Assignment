using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Api : MonoBehaviour
{
    private string URL = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public Text Label;
    public Text Points;
    public Text Addr;
    public Text Names;

    public int Index;

    void Start()
    {
        StartCoroutine(Getdata());
    }

    IEnumerator Getdata()
    {
        using(UnityWebRequest request= UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else{
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode Obj = SimpleJSON.JSON.Parse(json);

                Label.text =  Obj["clients"][Index]["label"] ;
                
                if(Index < 3){
                Points.text = "Points : " + Obj["data"][Index]["points"];
                Addr.text = "Address : " + Obj["data"][Index]["address"];
                Names.text = "Name : " + Obj["data"][Index]["name"];}
                else
                {
                Points.text = "Points : NILL";
                Addr.text = "Address : NILL";
                Names.text = "Name : NILL";
                }
            }
        }
    }

    
}
