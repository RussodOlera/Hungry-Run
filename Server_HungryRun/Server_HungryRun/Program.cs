using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Linq;
using System.Timers;
using System.Reflection.Emit;
using System.Threading;
using System.IO;


IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);
Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
listener.Bind(localEndPoint);
Encoding encoding = Encoding.ASCII;
bool status = false;
List<Socket> users= new List<Socket>();
Console.WriteLine("Benvenuti nel server di Hungry-Run!\n");

//Console.WriteLine(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
while (!status)
{
    Console.WriteLine("Comando: ");
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

void start()
{
    listener.Listen(100);
    Console.WriteLine("Server avviato");
    StartThread();
}

void StartThread()
{
    Thread t = new Thread(Service);
    t.Start();
}

void Service()
{
    string user_logged;
    Socket handler;
    handler = listener.Accept();
    users.Add(handler);
    StartThread();
    while(true)
    {
        string data = null;
        byte[] bytes = new byte[2048];
        byte[] toSend = null;
        try
        {
            int bytesRec = handler.Receive(bytes);
            data += encoding.GetString(bytes, 0, bytesRec);
            if (data.StartsWith("register"))
            {
                string[] value = data.Split(";");
                string path = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                File.AppendAllText(path + "/Users.txt", value[1] + ";" + value[2] + ";" + value[3] + ";" + value[4] + ";" + value[5] + ";" + value[6] + ";" + value[7] + ";" + value[8] + ";" + value[9] + ";" + value[10] + ";" + value[11] + ";" + value[12] + ";" + value[13] + "\n");
                toSend = encoding.GetBytes("File ricevuti dal Server!");
                handler.Send(toSend);
                Directory.CreateDirectory(path + "/" + value[12]);
            }
            else if (data.StartsWith("login"))
            {
                int cont = 0;
                bool result = false;
                string[] value = data.Split(";");
                string user = value[1];
                user_logged = user;
                string passwd = value[2];
                string[] temp;
                Console.WriteLine(user + " logged... " + "\n");
                string path = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                foreach (string line in File.ReadLines(path + "/Users.txt"))
                {
                    temp = line.Split(";");
                    if (temp[11] == user && temp[12] == passwd)
                        result = true;
                    cont++;
                }
                if (result)
                {
                    toSend = encoding.GetBytes("true" + ";" + user + ";" + passwd);
                    handler.Send(toSend);
                }
                else
                {
                    toSend = encoding.GetBytes("false");
                    handler.Send(toSend);
                }
            }
            else if (data.StartsWith("load_cart"))
            {
                List<string> cart = new List<string>();
                int t = 2;
                string[] value = data.Split(";");
                string[] temp = new string[value.Count()-2];
                string path = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/" + value[1] + "/Cart.txt";
                do
                {
                    if (File.Exists(path))
                    {
                        File.AppendAllText(path, value[t] + ";" + value[t + 1] + ";" + value[t + 2] + "\n");
                    }
                    else
                    {
                        StreamWriter f = new StreamWriter(path);
                        f.WriteLine(value[t] + ";" + value[t + 1] + ";" + value[t + 2]);
                        f.Close();
                    }
                    Console.WriteLine(value[t] + ";" + value[t + 1] + ";" + value[t + 2]);
                    t += 3;
                } while (t < value.Count());
            }
            else if(data.StartsWith("order"))
            {
                string[] value = data.Split(";");
                string order = null;
                int t = 1;
                string path = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                while(t < value.Count())
                {
                    if (t < value.Count() - 1)
                        order += value[t] + ";";
                    else
                        order += value[t];
                    t++;
                }
                Console.WriteLine(" New Order...");
                File.AppendAllText(path + "/Orders.txt", order );
                //File.Delete(System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/" + user_logged + "/Cart.txt");
            }
            else if(data.StartsWith("logout"))
            {
                string[] user = data.Split(";");
                Console.WriteLine(user[1]);
            }
        }
        catch
        {

        }
    }
}