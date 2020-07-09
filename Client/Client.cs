using System;
using System.Net.Sockets;
using System.Text;

namespace clientn
{
    class Program
    {

        static string HOST = "...";
        static int PORT = 9000;

        static TcpClient client;


        static void OpenConnection()
        {
            if(client != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---Connection is already open---");
            }
            else
            {
                try
                {
                    client = new TcpClient();
                    client.Connect(HOST, PORT);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Client is connected");

                }
                catch (Exception ex)
                {
                    client = null;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error : " + ex.Message);
                }
            }
        }


        static void CloseConnection()
        {
            if (client == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection is not open or already closed");
                return;
            }

            try
            {
                client.Close();

            }
            catch (Exception)
            {


            }
            finally
            {
                client = null;
            }


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Connection closed suuccesfull");

        }


        static void SendData(string data)
        {
            if (client == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection is altready closed");
                return;
            }

            //send
            NetworkStream stream = client.GetStream();
            byte[] byteToSend = ASCIIEncoding.ASCII.GetBytes(data);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sending : " + data);

            stream.Write(byteToSend, 0, byteToSend.Length);

            //receive
            byte[] byteToRead = new byte[client.ReceiveBufferSize];
            int byteRead = stream.Read(byteToRead, 0, client.ReceiveBufferSize);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(byteToRead, 0, byteRead));

        }

        static void Main(string[] args)
        {
            string lineRead;

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n Enter option (1-Open, 2-Connection, 3-Close, 4-Q)");
                lineRead = Console.ReadLine();
                switch (lineRead)
                {
                    case "1":
                        OpenConnection();
                        break;
                    case "2":
                        Console.WriteLine("Enter data to sned");
                        string data = Console.ReadLine();
                        SendData(data);
                        break;
                    case "3":
                        CloseConnection();
                        break;
                }

            } while (lineRead.Equals("4"));
        }
    }
}
