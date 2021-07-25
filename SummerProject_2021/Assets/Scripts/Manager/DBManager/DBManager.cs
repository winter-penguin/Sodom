/// +++++++++++++++++++++++++++++++++++++++++++++++++++
///  AUTHOR : Kim Jihun
///  Last edit date : 2021-07-23
///  Contact : kjhcorgi99@gmail.com
/// +++++++++++++++++++++++++++++++++++++++++++++++++++
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    [SerializeField] protected string phpFile;  //php 파일 이름
    protected string url; //LocalHost URL
    protected string queryResult;
    
/// <summary>
/// 데이터베이스 접속을 위한 세팅
/// </summary>
/// <param name="_phpFile">php 파일 이름</param>
    protected void Init(string _phpFile)
    {
        url = "http://220.127.167.244:8080/summerproject_2021/";
        phpFile = _phpFile;
    }

    /// <summary>
    /// 데이터베이스에 연결
    /// </summary>
    protected virtual IEnumerator ConnectDB()
    {
        using UnityWebRequest www = UnityWebRequest.Get(url + phpFile);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            //TODO: 서버 접속 실패시 에러 문구
            yield break;
        }

        queryResult = www.downloadHandler.text;
    }
    
    /// <summary>
    /// 테이블의 하나의 행을 여러 열로 구분
    /// </summary>
    /// <param name="data">구분하려고 하는 행</param>
    /// <param name="index">행의 이름 ex)"id:"</param>
    /// <param name="seperator">행을 열로 구분하기 위한 seperator</param>
    /// <returns>하나의 행의 index열 에 해당하는 값</returns>
    protected string GetDataValue(string data, string index, string seperator)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(seperator)) value = value.Remove(value.IndexOf(seperator));
        return value;

    }
    
}
