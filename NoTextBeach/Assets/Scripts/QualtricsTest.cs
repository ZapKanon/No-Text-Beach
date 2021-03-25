using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test class for linking values to a Qualtrics survey.
/// This script records if and how many times the spacebar was pressed before the survey is called with the enter key.
/// </summary>
public class QualtricsTest : MonoBehaviour
{
    private bool pressedSpace;
    private int spaceCounter;
    private string query;

    void Start()
    {
        pressedSpace = false;
        spaceCounter = 0;
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pressedSpace = true;
            spaceCounter++;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            // the first query sent must be preceded by "?" and all others after it need to start with "&"
            query = "&action1=" + pressedSpace + "&action2=" + spaceCounter;

            //WWWForm form = new WWWForm();
            //// Add the query to the end of the survey URL (with our added gamename variable)
            //UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get("https://rit.az1.qualtrics.com/jfe/form/SV_aVnF456JZzbo6rk?gamename=Beach" + query);

            //www.SendWebRequest();

            Application.OpenURL("https://rit.az1.qualtrics.com/jfe/form/SV_aVnF456JZzbo6rk?gamename=Beach" + query);

            //if (www.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || www.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            //{
            //    Debug.Log(www.error);
            //}
            //else
            //{
            //    Debug.Log(www.downloadHandler.text);
            //    Debug.Log("successful!");
            //    Debug.Log("pressedSpace = " + pressedSpace);
            //    Debug.Log("spaceCounter = " + spaceCounter);
            //}
        }
    }
}
