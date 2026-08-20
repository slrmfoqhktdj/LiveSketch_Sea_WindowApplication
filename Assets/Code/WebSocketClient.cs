using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

using WebSocketSharp;


[System.Serializable] 
public class Data 
{ 
    public string type; 
    public int id; 

} 

[System.Serializable] 
public class R_Data
{
    public string create;
    public string img;

    // public static R_Data returnValue(string jsonString)
    // {
    //     return JsonUtility.FromJson<R_Data>(jsonString);
    // }
}



public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    // Start is called before the first frame update
    void Start()
    {
        // ws = new WebSocket("ws://221.156.85.68:23232");// 127.0.0.1은 본인의 아이피 주소이다. 3333포트로 연결한다는 의미이다.
        ws = new WebSocket("ws://192.168.0.7:30001");
        //ws = new WebSocket("ws://gracetech.co.kr:30001");
        //  ws = new WebSocket("ws://121.178.114.99:20004");

        // ws = new WebSocket("ws://192.168.0.7:50001");
        ws.OnMessage += ws_OnMessage; //서버에서 유니티 쪽으로 메세지가 올 경우 실행할 함수를 등록한다.
        ws.OnOpen += ws_OnOpen;//서버가 연결된 경우 실행할 함수를 등록한다
        ws.OnClose += ws_OnClose;//서버가 닫힌 경우 실행할 함수를 등록한다.
        ws.Connect();//서버에 연결한다.
        // ws.Send("hello");//서버에게 "hello"라는 메세지를 보낸다.


    }

    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("onMessage : "+e.Data);//받은 메세지를 디버그 콘솔에 출력한다.

        R_Data d = JsonUtility.FromJson<R_Data>(e.Data);

        string[] b64 = d.img.Split(',');
        Debug.Log("image : "+b64);
        Spawner.create(0,b64[1]);

        // switch (e.Data)
        // {
        //     case "0":
        //         Spawner.create(0);
        //         break;

        //     case "1":
        //         Spawner.create(1);
        //         break;

        //     case "2":
        //         Spawner.create(2);
        //         break;

        //     case "3":
        //         Spawner.create(3);
        //         break;


        //     default:
        //         Debug.Log("지정되지 않은 생물");
        //         break;
        // }

    }


    void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("open"); //디버그 콘솔에 "open"이라고 찍는다.

        if(!ws.IsAlive){
            return;
        }else{
            Data data = new Data();
            data.type = "join";
            data.id = 0;




            ws.Send(JsonUtility.ToJson(data));
        }

  



    }
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("close"); //디버그 콘솔에 "close"이라고 찍는다.
    }




    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
