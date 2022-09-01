using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiTest : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.json?key=87b38f8f8d1ef09bfb1c84159d96cbea&targetDt=20120101"));
      // StartCoroutine(GetRequest("http://kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.json"));
        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(webRequest.error);
        }

        var result = webRequest.downloadHandler.text;
        Debug.Log(result);
        //string[] pages = uri.Split('/');
        //int page = pages.Length - 1;

        //switch (webRequest.result)
        //{
        //    case UnityWebRequest.Result.ConnectionError:
        //    case UnityWebRequest.Result.DataProcessingError:
        //        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
        //        break;
        //    case UnityWebRequest.Result.ProtocolError:
        //        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
        //        break;
        //    case UnityWebRequest.Result.Success:
        //        Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
        //        break;
        //}

    }
}
