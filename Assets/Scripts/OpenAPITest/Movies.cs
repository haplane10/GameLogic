using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movies : MonoBehaviour
{
    public BoxOfficeResult boxOfficeResult;
    public MovieDetailsInfo movieDetailsInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class DailyMovieInfo
{
    public string rnum;
    public string rank;
    public string rankInten;
    public string rankOldAndNew;
    public string movieCd;
    public string movieNm;
    public string openDt;
    public string salesAmt;
    public string salesShare;
    public string salesInten;
    public string salesChange;
    public string salesAcc;
    public string audiCnt;
    public string audiInten;
    public string audiChange;
    public string audiAcc;
    public string scrnCnt;
    public string showCnt;

}

[System.Serializable]
public class DailyOfficeInfo
{
    public string boxofficeType;
    public string showRange;
    public DailyMovieInfo[] dailyBoxOfficeList;
}

[System.Serializable]
public class BoxOfficeResult
{
    public DailyOfficeInfo boxOfficeResult;
}

[System.Serializable]
public class Actors
{
    public string peopleNm;
    public string peopleNmEn;
    public string cast;
    public string castEn;
}

[System.Serializable]
public class Directors
{
    public string peopleNm;
    public string peopleNmEn;
}

[System.Serializable]
public class MovieInfo
{
    public string movieCd;
    public string movieNm;
    public string movieNmEn;
    public string movieNmOg;
    public string showTm;
    public string prdtYear;
    public string openDt;
    public string prdtStatNm;
    public string typeNm;

    public Nations[] nations;
    public Genres[] genres;
    public Directors[] directors;
    public Actors[] actors;
    public ShowTypes[] showTypes;
    public Companys[] companys;
    public Audits[] audits;
    public Staffs[] staffs;
}

[System.Serializable]
public class Nations
{
    public string nationNm;
}

[System.Serializable]
public class Genres
{
    public string genreNm;
}

[System.Serializable]
public class ShowTypes
{
    public string showTypeGroupNm;
    public string showTypeNm;
}

[System.Serializable]
public class Companys
{
    public string companyCd;
    public string companyNm;
    public string companyNmEn;
    public string companyPartNm;
}

[System.Serializable]
public class Audits
{
    public string auditNo;
    public string watchGradeNm;
}

[System.Serializable]
public class Staffs
{
    public string peopleNm;
    public string peopleNmEn;
    public string staffRoleNm;

}

[System.Serializable]
public class MovieInfoResult
{
    public MovieInfo movieInfo;
}

[System.Serializable]
public class MovieDetailsInfo
{
    public MovieInfoResult movieInfoResult;
}