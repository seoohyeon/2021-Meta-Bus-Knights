
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

    // Port to receive data from Server
    [SerializeField] int rxPort = 8000;

    // port to send data to Server
    [SerializeField] int txPort = 8001;

    int i = 0; // DELETE THIS: Added to show sending data from Unity to Python via UDP

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread


    // 20210812_KDH player receiving magic data
    public string curMagicStr;
    public bool isreceivedData = false;


    IEnumerator SendDataCoroutine() // DELETE THIS: Added to show sending data from Unity to Python via UDP
    {
        while (true)
        {
            SendData("Sent from Unity: " + i.ToString());
            i++;
            yield return new WaitForSeconds(1f);
        }
    }

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
        print("UDP Comms Initialised");

        StartCoroutine(SendDataCoroutine()); // DELETE THIS: Added to show sending data from Unity to Python via UDP

        //StartCoroutine(initReceivedDataFlag());
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


                print(">> " + text);
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

    //Prevent crashes, close clients and threads properly
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }

}