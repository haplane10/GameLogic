using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class OpenAPI : MonoBehaviour
{
    public TMP_Text Results;
    public GameObject SearchConditions;
    public Button SearchButton;
    public GameObject ResultBoxButton;
    public Transform Content;
    public GameObject MovieDetailsBox;

    public TMP_Text Title;
    public TMP_Text Director;
    public TMP_Text Genre;
    public TMP_Text Main;

    [SerializeField] Toggle[] toggles;
    string URI_DailyBoxOffice;
    string URI_Week_WeekendBoxOffice;
    string URI_CommonCodeInquiry;
    string URI_MovieList;
    string URI_MovieDetailInformation;
    string URI_FilmCompanyList;
    string URI_FilmCompanyDetailInformation;
    string URI_MoviemanList;
    string URI_MoviemanDetailInformation;

    string apiKey;

    public InputField InputDate;
    public Movies movies;

    private void Awake()
    {
        //FileInfo jsonFile = new FileInfo("Assets/Scripts/JsonData.json");
        //string jsonStr = File.ReadAllText(jsonFile.FullName);
        //Movies movies = JsonUtility.FromJson<Movies>(jsonStr);
        //
        //Debug.Log(movies.boxOfficeResult.boxofficeType);

    }

    // Start is called before the first frame update
    void Start()
    {
        URI_DailyBoxOffice = "http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.json";
        URI_Week_WeekendBoxOffice = "http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchWeeklyBoxOfficeList.json";
        URI_CommonCodeInquiry = "http://kobis.or.kr/kobisopenapi/webservice/rest/code/searchCodeList.json";
        URI_MovieList = "http://kobis.or.kr/kobisopenapi/webservice/rest/movie/searchMovieList.json";
        URI_MovieDetailInformation = "http://www.kobis.or.kr/kobisopenapi/webservice/rest/movie/searchMovieInfo.json";
        URI_FilmCompanyList = "http://kobis.or.kr/kobisopenapi/webservice/rest/company/searchCompanyList.json";
        URI_FilmCompanyDetailInformation = "http://kobis.or.kr/kobisopenapi/webservice/rest/company/searchCompanyInfo.json";
        URI_MoviemanList = "http://kobis.or.kr/kobisopenapi/webservice/rest/people/searchPeopleList.json";
        URI_MoviemanDetailInformation = "http://kobis.or.kr/kobisopenapi/webservice/rest/people/searchPeopleInfo.json";

        apiKey = "78921d0e4154649862834a1532961d85";

        toggles = SearchConditions.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            Debug.Log(i);
            Toggle toggle = toggles[i];

            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
        }


        //StartCoroutine(GetRequest2("http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.json?key=78921d0e4154649862834a1532961d85&targetDt=20160902&repNationCd=K"));
        //StartCoroutine(GetRequest("http://www.kobis.or.kr/kobisopenapi/webservice/rest/boxoffice/searchDailyBoxOfficeList.json",
          //"20160902"));
    }

    void ToggleValueChanged(Toggle toggle)
    {
        //inputvalue 없을때?
        GameObject inputValue = toggle.transform.Find("InputValue").gameObject;

        if (toggle.isOn)
        {
            inputValue.SetActive(true);

            SearchButton.onClick.AddListener(() => OnSearchButton());
            
        }
        else
        {
            inputValue.SetActive(false);
        }
    }

    public void OnSearchButton()
    {
        string inputContent = InputDate.text;

        StartCoroutine(GetDailyBoxOffice(inputContent));
    }

    //IEnumerator GetWeekAndWeekendBoxOffice(string targetDt)
    //{
    //    UnityWebRequest uwr = UnityWebRequest.Get(URI_Week_WeekendBoxOffice + "?key=" + apiKey + "&targetDt=" + targetDt);
    //
    //    yield return uwr.SendWebRequest();
    //}


    //일별 박스오피스
    IEnumerator GetDailyBoxOffice(string targetDt)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(URI_DailyBoxOffice + "?key=" + apiKey + "&targetDt=" + targetDt);

        yield return uwr.SendWebRequest();

        movies.boxOfficeResult = JsonUtility.FromJson<BoxOfficeResult>(uwr.downloadHandler.text);

        var dailyList = movies.boxOfficeResult.boxOfficeResult.dailyBoxOfficeList;

        for (int i=0; i<dailyList.Length; i++)
        {
            GameObject rbb = Instantiate(ResultBoxButton, Content);
            TMP_Text content = rbb.GetComponentInChildren<TMP_Text>();

            content.text = dailyList[i].movieNm + "\n" + dailyList[i].openDt + "\n" + dailyList[i].movieCd;
            string movieCd = dailyList[i].movieCd;

            Button btn = rbb.GetComponent<Button>();
            btn.onClick.AddListener(() => StartCoroutine(GetMovieDetailInfo(movieCd)));

        }
    }

    //영화 상세정보
    IEnumerator GetMovieDetailInfo(string movieCd)
    {
        //영화상세정보
        UnityWebRequest uwr = UnityWebRequest.Get(URI_MovieDetailInformation + "?key=" + apiKey + "&movieCd=" + movieCd);
        
        yield return uwr.SendWebRequest();

        movies.movieDetailsInfo = JsonUtility.FromJson<MovieDetailsInfo>(uwr.downloadHandler.text);
        var movieInfo = movies.movieDetailsInfo.movieInfoResult.movieInfo;
        Title.text = movieInfo.movieNm;

        Director.text = "감독\n";
        for(int i=0; i<movieInfo.directors.Length; i++)
        {
            Director.text += movieInfo.directors[i].peopleNm;
            if (i < movieInfo.directors.Length - 1)
            {
                Director.text += " , ";
            }
        }

        Genre.text = "장르\n";
        for(int i=0; i<movieInfo.genres.Length; i++)
        {
            Genre.text += movieInfo.genres[i].genreNm;
            if(i < movieInfo.genres.Length - 1)
            {
                Genre.text += " , ";
            }
        }

        Main.text = "출연진\n\n";
        for(int i=0; i<movieInfo.actors.Length; i++)
        {
            Main.text += movieInfo.actors[i].peopleNm;
            if (i > 10)
            {
                break;
            }
            if (i < movieInfo.actors.Length - 1)
            {
                Main.text += " , ";
            }
        }
        MovieDetailsBox.SetActive(true);
    }

    public void OnXButton()
    {
        MovieDetailsBox.SetActive(false);
    }

}
