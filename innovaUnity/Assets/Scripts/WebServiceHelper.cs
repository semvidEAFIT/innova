using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;


public class WebServiceHelper{

    public static string callServiceGet(string serviceURL, Dictionary<string, object> parameters) {
        string getData = "?";
        foreach (KeyValuePair<string, object> kv in parameters)
        {
            getData += kv.Key + "=" + kv.Value + "&";
        }
        getData = getData.Substring(0, getData.Length - 1);
        WebRequest request = WebRequest.Create(serviceURL + getData);
        WebResponse response = request.GetResponse();
        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        return responseString;
    }

    public static string callServicePost(string serviceURL, Dictionary<string, object> parameters) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceURL);
        UTF8Encoding encoding = new UTF8Encoding();
        string postData = "";
        foreach(KeyValuePair<string, object> kv in parameters){
            postData += kv.Key + "=" + kv.Value+"&";
        }
        postData = postData.Substring(0, postData.Length - 1);
        byte[] byteArray = encoding.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;

        using (Stream stream = request.GetRequestStream())
        {
            stream.Write(byteArray, 0, byteArray.Length);
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        return responseString;
    }
}
