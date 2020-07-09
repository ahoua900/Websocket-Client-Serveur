using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Discussion interface client/server open!");
            Socket socketMaster = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("192.168.70.84"), 7000);
            socketMaster.Connect(ipEnd);

            byte[] buffer = new byte[socketMaster.ReceiveBufferSize];
            int readAllByte;

            string sendData = "";
            do
            {
                Console.Write(">>");
                sendData = Console.ReadLine();
                socketMaster.Send(System.Text.Encoding.UTF8.GetBytes(sendData));

                //pour la reception
                readAllByte = socketMaster.Receive(buffer);
                byte[] readData = new byte[readAllByte];

                //copy et convertion du message en form originel
                Array.Copy(buffer, readData, readAllByte);



                //get piggy sata
                //byte[] pdt = new byte[4];
                //socketMaster.Receive(pdt);

                //affichage de votre piggyback data
                Console.WriteLine("Response server: {0}", System.Text.Encoding.UTF8.GetString(readData));

                var sendMessage = Console.ReadLine();
                socketMaster.Send(System.Text.Encoding.UTF8.GetBytes(sendMessage));

            } while (sendData.Length > 0);

            socketMaster.Close();

        }
    }
}
