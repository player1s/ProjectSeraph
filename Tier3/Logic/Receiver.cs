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
        // Get Host IP Address that is used to establish a connection  
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
        // If a host has multiple addresses, you will get a list of addresses  
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        
  
        try {   
  
            // Create a Socket that will use Tcp protocol      
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
            // A Socket must be associated with an endpoint using the Bind method  
            listener.Bind(localEndPoint);  
            // Specify how many requests a Socket can listen before it gives Server busy response.  
            // We will listen 10 requests at a time  
            listener.Listen(10);  
  
            Console.WriteLine("Waiting for a connection...");  
            Socket handler = listener.Accept();  
  
             // Incoming data from the client.    
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
