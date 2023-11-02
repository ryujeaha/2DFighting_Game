using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public InputField In_create_InPut_port;
    private Socket m_socket = null; 
    private State m_state;

    public int port;
    private Socket m_listener = null;
    public Text Ip_Text_host;
    enum State
    {
        don_Start ,
        StartListener,
        AcceptClient,
        ServerCommunication,
        StopListener,
        EndCommunication,
    }


    private void Start() {
        m_state = State.don_Start;
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in host.AddressList){
           Ip_Text_host.text= ip.ToString();
        }
    }

    void Update()
    {
        switch(m_state)
        {
            case State.don_Start:
                don_Start();
                break;
            case State.StartListener:
                create_take_Value();
                break;
            case State.AcceptClient:
                AcceptClient();
                break;
            case State.ServerCommunication:
                ServerCommunication();
                break;
            case State.StopListener:
                StopListener();
                break;
            case State.EndCommunication:
                break;
            default:
                break;
        }
    }

    void don_Start(){
    }

    public void Start_Create(){
        Debug.Log("시작");
        m_state = State.StartListener;
       
    }

    void create_take_Value(){
        port =int.Parse(In_create_InPut_port.GetComponent<InputField>().text);
        m_listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        m_listener.Bind(new IPEndPoint(IPAddress.Any,port));
        m_listener.Listen(1);
        Debug.Log("값");
        m_state = State.AcceptClient;
    }

    void AcceptClient()
    {
        if(m_listener != null && m_listener.Poll(0, SelectMode.SelectRead)){
            m_socket = m_listener.Accept();
            Debug.Log("연결");
            m_state = State.ServerCommunication;
        }
    }

    void ServerCommunication(){
        Debug.Log("커뮤");
        byte[] buffer = new byte[1400];
        IPEndPoint sender = new IPEndPoint(IPAddress.Any,0);
        EndPoint SenderRemote = sender;
        int recvSize = m_socket.Receive(buffer, buffer.Length, SocketFlags.None);
        if(recvSize > 0){
            string message = System.Text.Encoding.UTF8.GetString(buffer);
            string packetType = message.Split('#')[0];
            string packetData = message.Split('#')[1];
            Debug.Log(message);
            switch(packetType){
                case "PlayerInfo":
                    PlayerInfo playerInfo = new PlayerInfo();
                    playerInfo = JsonUtility.FromJson<PlayerInfo>(packetData);
                    Debug.Log(string.Format("닉네임 : {0},IP : {1},  Port : {2}",playerInfo.Nick_name,playerInfo.
                    playeraddress,playerInfo.player_Port));
                   break;
                case"ControllInfo":
                    ControllInfo cInfo = new ControllInfo();
                    cInfo = JsonUtility.FromJson<ControllInfo>(packetData);
                    break; 
                default:
                    Debug.Log("정의되지 않은 패킷입니다. : "+ packetData);
                    break;
            }
        }
    }

    void StopListener(){
        if(m_listener != null){
            m_listener.Close();
            m_listener = null;
        }

        m_state = State.EndCommunication;
    }

    void EndCommunication(){

    }
}
