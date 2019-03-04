using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;

namespace Tier3
{
    public class Receiver
    {
        string data = null;  

        public Receiver()
        {

        }

        public string StartServer()  
    {  
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        
        try {   
  
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);  
            listener.Listen(10);  
  
            Console.WriteLine("Waiting for a connection...");

            Socket handler = listener.Accept();  
  
            byte[] bytes = null;                
            bytes = new byte[102400];  
            int bytesRec = handler.Receive(bytes);  
            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);                               
            
            handler.Shutdown(SocketShutdown.Both);  
            handler.Close(); 

        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        }  
  
            return data;             
        }          
    }  
}
