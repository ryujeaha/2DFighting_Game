using System.Collections;
using System.Collections.Generic;
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

    bool ip_on;
    bool port_on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ip_on_(){
        ip_on = true;
    }
    public void port_on_(){
        port_on = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ip_on&&port_on){
            value_taken();
        }
    }
    void value_taken(){
        
    }
    
}
