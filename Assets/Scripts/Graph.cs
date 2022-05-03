using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Connection with Python
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class Graph : MonoBehaviour
{

    // Setting for socket
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    private TcpClient client;
    private Thread mThread;
    private NetworkStream nwStream;
    private static Vector3 receivedPos = new Vector3(0, 0, 0);
    bool running;
    // Intialze Trajectory
    [SerializeField]
    Transform CubePrefab = default;
    int startPoint = 3;
    int plotLength = 10;
    float step = 1f;
    int resolution = 500;
    Transform[] points;
    float[] posY;


    // Start is called before the first frame update
    void Start()
    {
        //start thread(Get data from Python)
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();
        // Initial Trajectory
        var position = Vector3.zero;
        var scale = Vector3.one * 0.15f;
        points = new Transform[resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform Cube = Instantiate(CubePrefab);
            position.x = i * step + startPoint;
            Cube.localPosition = position;
            Cube.localScale = scale;
            Cube.SetParent(transform);
            points[i] = Cube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float initTime = receivedPos.x;

        for (int i = 0; i < points.Length; i++)
        {
            Transform Cube = points[i];
            Vector3 position = Cube.localPosition;
            position.y = receivedPos.z;
            Cube.localPosition = position;
        }
        print(initTime);
    }
    void GetInfo()
    {
        client = new TcpClient(connectionIP, connectionPort);
        running = true;
        while (running)
        {
            SendAndReceiveData();
        }
    }

    void SendAndReceiveData()
    {
        nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {
            //---Using received data---
            receivedPos = StringToVector3(dataReceived); //<-- assigning receivedPos value from Python
            //---Sending Data to Host----
            byte[] myWriteBuffer = Encoding.ASCII.GetBytes(receivedPos.y.ToString()); //Converting string to byte data
            nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python
        }
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

}
