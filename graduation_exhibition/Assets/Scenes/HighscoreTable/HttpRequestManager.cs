using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Net;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Signup {
    public string u_id;
    public string email;
    public string password;
}

[System.Serializable]
public class LogInOut {
    public string email;
    public string password;
}

[System.Serializable]
public class UserInfo {
    public string u_id;
    public int role;
    public string name;
}

[System.Serializable]
public class WorkInfo {
    public string u_id;
    public float position_x;
    public float position_y;
    public string file_addr;
    public string title;
    public string desc;
}

[System.Serializable]
public class U_ID {
    public string u_id;
}


public class HttpRequestManager : MonoBehaviour
{
    private static string baseURL = "http://3.38.73.175:80";
    void Start() {
        Status status_ = Signup("as1", "111", "asa");

        Debug.Log(status_.status);   

        //LikeScoreList list = GetLikeScoreList("1");
        //Debug.Log(list.list.Count);
    }

    private string post(string data, string endPoint) {
        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + endPoint);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;
        
        var reqStream = request.GetRequestStream();
        reqStream.Write(bytes, 0, bytes.Length);
        reqStream.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream respStream = response.GetResponseStream();
        StreamReader resStream = new StreamReader(respStream);
        string json = resStream.ReadToEnd();
        resStream.Close();

        return json;
    }

    private string get(string endPoint) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + endPoint);
        request.Method = "GET";
        request.ContentType = "application/json";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream respStream = response.GetResponseStream();
        StreamReader resStream = new StreamReader(respStream);
        string json = resStream.ReadToEnd();
        resStream.Close();

        return json;
    }

    private string put(string data, string endPoint) {
        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + endPoint);
        request.Method = "PUT";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;
        
        var reqStream = request.GetRequestStream();
        reqStream.Write(bytes, 0, bytes.Length);
        reqStream.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream respStream = response.GetResponseStream();
        StreamReader resStream = new StreamReader(respStream);
        string json = resStream.ReadToEnd();
        resStream.Close();

        return json;
    }
    public Status Signup(string u_id, string email, string password) {
        // request data
        Signup data = new Signup();
        data.u_id = u_id;
        data.email = email;
        data.password = password;
        string str = JsonUtility.ToJson(data);

        string json = post(str, "/user");
        
        Status obj = new Status();
        obj = JsonUtility.FromJson<Status>(json);
        
        return obj;
    }

    public User LogIn(string email, string password) {
        // request data
        LogInOut data = new LogInOut();
        data.email = email;
        data.password = password;

        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);

        string json = post(str, "/user/login");
        
        User obj = new User();
        obj = JsonUtility.FromJson<User>(json);
        
        return obj;
    }

    public Status LogOut(string email, string password) {
        // request data
        LogInOut data = new LogInOut();
        data.email = email;
        data.password = password;

        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);
        string json = post(str, "/user/logout");
        
        Status obj = new Status();
        obj = JsonUtility.FromJson<Status>(json);
        
        return obj;
    }

    public Status EnterUserInfo(string u_id, int role, string name) {
        // request data
        UserInfo data = new UserInfo();
        data.u_id = u_id;
        data.role = role;
        data.name = name;

        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);
        string json = put(str, "/user/detail");

        Status obj = new Status();
        obj = JsonUtility.FromJson<Status>(json);
        
        return obj;
    }

    public Status EnterWorkInfo(string u_id, float position_x, float position_y, string file_addr, string title, string desc) {
        // request data
        WorkInfo data = new WorkInfo();
        data.u_id = u_id;
        data.position_x = position_x;
        data.position_y = position_y;
        data.file_addr = file_addr;
        data.title = title;
        data.desc = desc;


        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);
        string json = post(str, "/work");
        
        Status obj = new Status();
        obj = JsonUtility.FromJson<Status>(json);
        
        return obj;
    }
    public WorkInfomation GetWorkInfo(string u_id) {
        // request data
        U_ID data = new U_ID();
        data.u_id = u_id;

        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);
        string json = post(str, "/work/get");
        
        WorkInfomation obj = new WorkInfomation();
        obj = JsonUtility.FromJson<WorkInfomation>(json);
        
        return obj;
    }
    public WorkLike UpdateLike(string u_id) {
        // request data
        U_ID data = new U_ID();
        data.u_id = u_id;

        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string str = JsonUtility.ToJson(data);
        string json = put(str, "/like");
        
        WorkLike obj = new WorkLike();
        obj = JsonUtility.FromJson<WorkLike>(json);
        
        return obj;
    }
    public LikeScoreList GetLikeScoreList()
    {
        // json utility를 사용해 json 으로 변환한 뒤 byte로 변환
        string json = get("/like/list");

        LikeScoreList obj = new LikeScoreList();
        obj = JsonUtility.FromJson<LikeScoreList>(json);

        return obj;
    }
}
