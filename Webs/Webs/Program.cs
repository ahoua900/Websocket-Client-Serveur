using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Webs
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            //creation du socket server


            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("192.168.70.84"), 7000);
            listenerSocket.Bind(ipEnd);




            while (true)
            {
                listenerSocket.Listen(0);
                Socket clienSocket = listenerSocket.Accept();


                //multi client
                Thread clientThread = new Thread(() => ClientConnecte(clienSocket));
                clientThread.Start();

            }

        }


        //method de connection call by thread

        private static void ClientConnecte(Socket clienSocket)
        {
            //taille  en byte du message 
            byte[] buffer = new byte[clienSocket.ReceiveBufferSize];



            int readAllByte;

            do
            {
                //Entrer du mlessage en byte
                readAllByte = clienSocket.Receive(buffer);

                //preparattion d'une copy
                byte[] readData = new byte[readAllByte];


                //copy et convertion du message en form originel
                Array.Copy(buffer, readData, readAllByte);


                //affichage a la console
                Console.WriteLine("From client: {0}", System.Text.Encoding.UTF8.GetString(readData));


                //piuggyback data
                var sendMessage = Console.ReadLine();
                clienSocket.Send(System.Text.Encoding.UTF8.GetBytes(sendMessage));

            } while (readAllByte > 0);


            //en dehors de boucle le client est deconnecte
            Console.WriteLine("Client disconneted");
            Console.ReadKey();
        }
    }
}
