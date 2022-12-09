using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Security.Principal;
using System.Reflection.Emit;

namespace ConsoleApp1
{
    public class SynchronousSocketListener
    {
        internal class Program
        {
            public static string data = null;
            public IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            public IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);
            public Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            public static void start()
            {
                Encoding encod = Encoding.ASCII;
                listener.Listen(10);
                Console.WriteLine("Server avviato");
                Console.WriteLine("In attesa di connessione...");
                byte[] bytes = new Byte[1024];
                
                try
                {
                    listener.Bind(localEndPoint);
                    while (true)
                    {
                        Console.WriteLine("In attesa di connessione...");
                        Socket handler = listener.Accept();
                        data = null;
                        while (true)
                        {
                            int bytesRec = handler.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (data.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Messaggio del Client: "+ data);
                        string risp = "Ciao Programma";
                        byte[] msg = Encoding.ASCII.GetBytes(risp);

                        handler.Send(msg);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("\nPress ENTER to continue...");
                Console.Read();
            }
            static void Main(string[] args)
            {
                bool status = false; 
                Console.WriteLine("Server di Hungry-Run!\n");
                while (!status)
                {
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "start":
                            status = true;
                            start();
                            break;
                        default:
                            Console.WriteLine("commando errato\n");
                            break;
                    }
                }
            }
        }
    }

}
