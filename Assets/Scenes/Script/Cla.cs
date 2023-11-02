using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class Cla : MonoBehaviour
{
    private Socket m_socket = null;
    private string m_adress;
    private int m_port;
    public InputField jo_Input_ip;
    public InputField jo_Input_port;
    public InputField po_Input;
    public InputField name_Input;

 public GameObject Info_input;
public GameObject Btn;
PlayerInfo Info;
    // Start is called before the first frame update
    void Start()
    {   
        Info = new PlayerInfo();
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in host.AddressList){
           Info.playeraddress = ip.ToString();
        }
    }

    public void taken_Start(){
        
        Info.Nick_name = name_Input.text;
        Info.player_Port = int.Parse(po_Input.text);
        value_taken(Info);  
    }
        
    void value_taken(PlayerInfo pInfo){
        if(jo_Input_ip.text != null &&jo_Input_port.text != null){
            m_socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            
            string data = JsonUtility.ToJson(pInfo);
            m_socket.NoDelay = true;    
            m_socket.SendBufferSize = 0;
            m_socket.Connect(jo_Input_ip.text,int.Parse(jo_Input_port.text));
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(string.Format("PlayerInfo#{0}",data));
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(jo_Input_ip.text),int.Parse(jo_Input_port.text));
            m_socket.SendTo(buffer,buffer.Length,SocketFlags.None,endPoint); 
        }
        
    }   

    public void finish_info(){
    Info_input.SetActive(false);
    Btn.SetActive(true);
   }
    
}
