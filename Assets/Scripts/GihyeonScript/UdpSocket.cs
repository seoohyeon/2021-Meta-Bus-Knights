using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpSocket : MonoBehaviour
{

    [HideInInspector] public bool isTxStarted = false;

    // Local host
    [SerializeField] string IP = "192.168.25.38";

    // Port : receive data from Server
    [SerializeField] int rxPort = 8000;

    // Port : send data to Server
    [SerializeField] int txPort = 8001;

    // Create UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; 

    // 20210812_KDH player receiving magic data
    public string curMagicStr;
    public bool isreceivedData = false; 

    // Function : send data to server
    public void SendData(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    private void Update()
    {
        // 매 프레임마다 값을 false로 갱신
        isreceivedData = false;
    }

    void Awake()
    {
        // Create remote endpoint
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));

        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialization
        print("Udp Communications Starting...");


    }

    // Function : receive data, update packets received
    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);

                // 20210812_KDH send magic kind to player
                curMagicStr = text;
                ChangeIsreceivedData();

                ProcessInput(text);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ChangeIsreceivedData()
    {
        isreceivedData = true;   
    }


    private void ProcessInput(string input)
    {
        // First data arrived, so tx started
        if (!isTxStarted) 
        {
            isTxStarted = true;
        }
    }

    //Prevent crashes, Close threads and clients
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }

}