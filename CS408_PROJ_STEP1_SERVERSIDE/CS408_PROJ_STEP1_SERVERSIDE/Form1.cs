using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace CS408_PROJ_STEP1_SERVERSIDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static bool listening = false;
        static bool terminating = false;
        static bool accept = true;
        static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static List<Socket> socketList = new List<Socket>();
        static List<string> usernameList = new List<string>();
        static List<string> playerList = new List<string>(); //used to keep track of players in the game
        static List<string> playerListTemp = new List<string>(); //used to keep track of players in the game
        static int counter = 0;

        public struct Clients
        {

            public Socket clientSocket;
            public string userName;
            //public bool isConnected;

            public Clients(Socket p2, string name)
            {
                //isConnected = g;
                clientSocket = p2;
                userName = name;
            }
        }

        public struct Games
        {
            public string player1;
            public string player2;
            
            public Games(string p1, string p2)
            {
                player1 = p1;
                player2 = p2;                
            }
        }

        public class GlobalPoints
        {
            private int playerPoint;
            private string playerName;

            public GlobalPoints(string name)
            {
                playerPoint = 0;
                playerName = name;
            }

            public int getPoints() 
            {
                return playerPoint;
            }
            public string getPlayerName()
            {
                return playerName;
            }
            public void addPoints()
            {
                playerPoint++;
            }
            public bool isDaName(string tempName)
            {
                return tempName == playerName;   
            }
        }

        static List<GlobalPoints> globalPointList = new List<GlobalPoints>();

        public void deleteFromGlobalPointList(string tempname)
        {
            foreach(GlobalPoints tempGlobalPoint in globalPointList)
            {
                if(tempGlobalPoint.isDaName(tempname))
                {
                    globalPointList.Remove(tempGlobalPoint);
                    break;
                }
            }
        }

        public void increaseGlobalPointbyName(string tempname)
        {
            foreach (GlobalPoints tempGlobalPoint in globalPointList)
            {
                if (tempGlobalPoint.isDaName(tempname))
                {
                    tempGlobalPoint.addPoints();
                    break;
                }
            }
        }

        public class Guess
        {
            public string player1;
            public string player2;
            public int guess1;
            public int guess2;
            public int point1;
            public int point2;
            public int round;
            public int serversGuess;

            public Guess(string pl1 = " ", string pl2 = " ",int g1 = -1, int g2 = -1, int p1 = 0, int p2 = 0, int r = 0, int s = -1) 
            { // -1 is going to be used for no guess yet
                player1 = pl1;
                player2 = pl2;                
                guess1 = g1;
                guess2 = g2;
                point1 = p1;
                point2 = p2;
                round = r;
                serversGuess = s;
            }
        }

        static List<Guess> guessList = new List<Guess>();

        static List<Games> GamerList = new List<Games>();
        static List<Games> GamerListTemp = new List<Games>();

        static List<Clients> ClientListStruct = new List<Clients>();

        public void sendPlayerList(Socket clientSocket) //to send player list to the client whom requested
        {
            string initialMessage = "Here are the players in the room : \n";
            //richTextBox1.AppendText(initialMessage);
            byte[] bufferTempInitial = Encoding.Default.GetBytes(initialMessage);
            clientSocket.Send(bufferTempInitial);

            foreach (GlobalPoints tempGlobal in globalPointList)
            {
                string tempPrePrint = "     >> " + tempGlobal.getPlayerName() + " with global point " + tempGlobal.getPoints() +"\r\n";
                byte[] bufferTemp = Encoding.Default.GetBytes(tempPrePrint);
                clientSocket.Send(bufferTemp);
            }
            //foreach (string s in usernameList) //send every player in the username list
            //{
            //    string tempPrePrint = "     >> " + s + "\r\n";
            //    byte[] bufferTemp = Encoding.Default.GetBytes(tempPrePrint);
            //    clientSocket.Send(bufferTemp);
            //    //richTextBox1.AppendText(tempPrePrint);
            //}

        }

        public bool addClient(Socket clientTemp) // adding new client if the username is not previously used
        {
            string username;
            Byte[] buffer = new byte[64];
            int rec = clientTemp.Receive(buffer);
            username = Encoding.Default.GetString(buffer);
            username = username.Substring(0, username.IndexOf("\0"));
            if (usernameList.Contains(username)) //adds if user name is not used
            {
                richTextBox1.AppendText("New client request have been rejected. \n");
                richTextBox1.AppendText("Following username is already used :" + username +"\n");
                return false;
            }
            else
            {
                globalPointList.Add(new GlobalPoints(username));
                richTextBox1.AppendText(globalPointList[0].getPlayerName());
                usernameList.Add(username);
                Clients tempClient = new Clients(clientTemp, username);
                ClientListStruct.Add(tempClient);
                richTextBox1.AppendText("New client has connected : " + username + "\n");                
                return true;
            }

        }

        public string returnUserName(Socket clientTemp) //returns the user name in the struct
        {
            string temp;
            for (int i = 0; i < ClientListStruct.Count; i++)
            {
                if (ClientListStruct[i].clientSocket == clientTemp)
                {
                    temp = ClientListStruct[i].userName;
                    return temp;
                }
            }
            return "No name found!";
        }


        public string returnOpponentName(List<Games> gamesList, string opponent)
        {
            string temp;
            for (int i = 0; i < gamesList.Count; i++)
            {
                if (gamesList[i].player1 == opponent)
                {
                    temp = gamesList[i].player2;
                    //gamesList.Remove(gamesList[i]);
                    return temp;
                }
                else if (gamesList[i].player2 == opponent)
                {
                    temp = gamesList[i].player1;
                    //gamesList.Remove(gamesList[i]);
                    return temp;
                }
            }
            return "No name found!";
        }

        //public void deleteFromTemp(string playerOne, string playerTwo) //to keep temp list empty all the time
        //{ //could have also said remove all ?? mayber better??
        //    playerListTemp.Remove(playerOne);
        //    playerListTemp.Remove(playerTwo);
        //    for (int i = 0; i < GamerListTemp.Count; i++)
        //    {
        //        if (GamerListTemp[i].player1 == playerOne || GamerListTemp[i].player2 == playerOne ||
        //           GamerListTemp[i].player1 == playerTwo || GamerListTemp[i].player2 == playerTwo)
        //        {
        //            GamerListTemp.Remove(GamerListTemp[i]);
        //        }
        //    }
        //}

        public void initiateGame(string requester, string requested)
        {
            if (!(usernameList.Contains(requested))) //if requested players is not present in network
            {
                richTextBox1.AppendText("Player : " + requested + " who is requested by " + requester + " is not connected to server. \n");
            }
            else if (playerList.Contains(requester))
            {
                richTextBox1.AppendText("Player : " + requester + " is already joined a game. \n");
            }
            else if (playerList.Contains(requested))
            {
                richTextBox1.AppendText("Player : " + requested + " is already joined a game. \n");
            }
            else //in case of invitation; the pair of players are put in temporary list to be later added to real list if other player accepts else it is removed from the list
            {
                playerListTemp.Add(requested);
                playerListTemp.Add(requester);
                Games tempGame;
                tempGame.player1 = requester;
                tempGame.player2 = requested;
                GamerListTemp.Add(tempGame);
            }
        }

        public void sendMessageByName(string nameOfPlayer, string message)
        {
            Socket tempOpponentSocket;
            for (int i = 0; i < ClientListStruct.Count; i++) //to return socket of the opponent
            {
                if (ClientListStruct[i].userName == nameOfPlayer)
                {
                    tempOpponentSocket = ClientListStruct[i].clientSocket;
                    sendMessage(tempOpponentSocket, message);
                }
            }
        }

        public void removeGamelist(List<Games> gamesList, List<string> playerList,  string playerOne, string playerTwo)
        {
            if (playerList.Contains(playerOne))
            {
                if (playerTwo == " ")
                {
                    playerTwo = returnOpponentName(gamesList, playerOne);
                }
                for (int i = 0; i < gamesList.Count; i++)
                {
                    if (gamesList[i].player1 == playerOne || gamesList[i].player2 == playerOne ||
                        gamesList[i].player1 == playerTwo || gamesList[i].player2 == playerTwo)
                    {
                        gamesList.Remove(gamesList[i]);
                    }
                }
                playerList.Remove(playerOne);
                playerList.Remove(playerTwo);
            }
        }

        public void removeClientList(List<Clients> clientsList, List<string> clientNameList, string clientName)
        {
            clientNameList.Remove(clientName);
            for ( int i = 0; i < clientsList.Count; i++)
            {
                if(clientsList[i].userName == clientName)
                {
                    clientsList.Remove(clientsList[i]);
                }
            }
        }

        public void removeClientList(List<Clients> clientsList, List<string> clientNameList, Socket clientSocket)
        {
            removeGamelist(GamerList, playerList, returnUserName(clientSocket), " "); //first deleted from playerlists
            removeGamelist(GamerListTemp, playerListTemp, returnUserName(clientSocket), " ");
            if (socketList.Contains(clientSocket) && clientNameList.Contains(returnUserName(clientSocket)) ) // Precaution
            {                
                clientNameList.Remove(returnUserName(clientSocket));
            }            
            for (int i = 0; i < clientsList.Count; i++)
            {
                if (clientsList[i].clientSocket == clientSocket)
                {
                    clientsList.Remove(clientsList[i]);
                }
            }
           
        } // Client is also removed from gamer list of any type

        public void invitePlayer(Socket theSocket, string message)
        {
            if (message.Length > 6)
            {
                string tempMessage =  message.Substring(0, 6);
                if (tempMessage == "Invite")
                {
                    int endOfString = message.Length - 6;
                    string tempPlayer = message.Substring(6, endOfString);
                    string tempUsername = returnUserName(theSocket);
                    if (tempUsername != tempPlayer)
                    {
                        richTextBox1.AppendText(tempUsername + " is asking to invite : " + tempPlayer + ". \n");
                        if (usernameList.Contains(tempPlayer))
                        {
                            if (playerList.Contains(tempPlayer)) //if the person being invited is already playing
                            {
                                sendMessage(theSocket, tempPlayer + " is already been playing with someone else. \n");
                                richTextBox1.AppendText(tempPlayer + " is already playing. \n");
                            }
                            else if (playerList.Contains(tempUsername)) //if person who is inviting is already playing
                            {
                                sendMessage(theSocket, "You are already in a game! So you can't invite anybody. \n");
                                richTextBox1.AppendText(tempUsername + " is already playing. \n");
                            }
                            else //both of them are not playing thus the invitation is sent
                            {
                                sendMessageByName(tempPlayer, tempUsername + " is inviting you to play. \n");
                                sendMessage(theSocket, "Your invitation messege has been sent to " + tempPlayer + " .\n");
                                initiateGame(tempUsername, tempPlayer);
                            }
                        }
                        else //if player that is being invited is not exists
                        {
                            richTextBox1.AppendText(tempUsername + " can't invite " + tempPlayer + " because s/he is not present in lobby. \n");
                        }
                    }
                }
            }
        } 

        public void acceptGame(Socket theSocket)
        {
            string tempUsername = returnUserName(theSocket);
            string temp;
            if (playerListTemp.Contains(tempUsername))
            {
                for (int i = 0; i < GamerListTemp.Count; i++)
                {
                    if (GamerListTemp[i].player1 == tempUsername)
                    {
                        temp = GamerListTemp[i].player2;
                        sendMessage(theSocket, "You can't accept your own request. \n");
                        richTextBox1.AppendText(tempUsername + " can't accept its own request to start game with " + temp + ". \n");
                    }
                    else if (GamerListTemp[i].player2 == tempUsername)
                    {
                        temp = GamerListTemp[i].player1;
                        playerList.Add(temp);
                        playerList.Add(tempUsername);
                        Games RealGame = new Games(temp, tempUsername);
                        GamerList.Add(RealGame);
                        removeGamelist(GamerListTemp, playerListTemp, temp, tempUsername); //remove from temp                           
                        sendMessageByName(temp, "You are in a new game with " + tempUsername + ". \n");
                        sendMessage(theSocket, "You are in a new game with " + temp + ". \n");
                        richTextBox1.AppendText(temp + " and " + tempUsername + " are in a new game. \n");
                    }
                }
            }
            else
            {
                richTextBox1.AppendText("There is no request for user: " + tempUsername + " . \n");
                sendMessage(theSocket, "There is no request for you. Thus you can't accept.\n");
            }
        }

        public void rejectGame(Socket theSocket)
        {
            string tempUsername = returnUserName(theSocket);
            string temp;
            if (playerListTemp.Contains(tempUsername))
            {
                for (int i = 0; i < GamerListTemp.Count; i++)
                {
                    if (GamerListTemp[i].player1 == tempUsername)
                    {
                        temp = GamerListTemp[i].player2;
                        sendMessage(theSocket, "You can't reject your own request. \n");
                        richTextBox1.AppendText(tempUsername + " can't reject its own request to start game with " + temp + ". \n");
                        break;
                    }
                    else if (GamerListTemp[i].player2 == tempUsername)
                    {
                        temp = GamerListTemp[i].player1;
                        removeGamelist(GamerListTemp, playerListTemp, temp, tempUsername); //remove from temp    
                        sendMessageByName(temp, "You have been rejected by " + tempUsername + " .\n");
                        sendMessage(theSocket, "You have rejected player: " + temp + ". \n");
                        richTextBox1.AppendText(tempUsername + " has rejected the request from " + temp + ". \n");
                        break;
                    }
                }
            }
            else
            {
                richTextBox1.AppendText("There is no request for user: " + tempUsername + ". \n");
                sendMessage(theSocket, "You can't reject. Because you are not invited. \n");
            }
        }
        // at global points

        public int makeRandomChoice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 100);
        }
        // Global score for each player. Give global point when game is over.
        // and delete global point function. 
        public void deleteInGuessList(Guess tempGuess)
        {
            removeGamelist(GamerList, playerList, tempGuess.player1, tempGuess.player2);
            if (guessList.Contains(tempGuess))
            {
                guessList.Remove(tempGuess);
            }

        }

        public void deleteFromGuessListByName(string player)
        {
            foreach(Guess tempGuess in guessList)
            {
                if(tempGuess.player1 == player)
                {
                    
                    richTextBox1.AppendText(tempGuess.player1 + " has disconnected or surrendered thus lost the game and so " + tempGuess.player2 + " has won the game. \n");
                    string tempName = tempGuess.player2;
                    deleteInGuessList(tempGuess);
                    removeGamelist(GamerList, playerList, player, tempName);
                }
                else if(tempGuess.player2 == player)
                {
                    richTextBox1.AppendText(tempGuess.player1 + " has disconnected or surrendered thus lost the game and so " + tempGuess.player1 + " has won the game. \n");
                    string tempName = tempGuess.player1;
                    deleteInGuessList(tempGuess);
                    removeGamelist(GamerList, playerList, player, tempName);
                }
            }
        }

        public void checkRound(Guess tempGuess)
        {
            if (tempGuess.round > 1)
            {
                if(tempGuess.point1 == tempGuess.point2)
                { //no winner yet

                }
                else if(tempGuess.point1 > tempGuess.point2)
                {
                    richTextBox1.AppendText("It has been " +tempGuess.round +" between " + tempGuess.player1 + " and "
                        + tempGuess.player2 + ". Now game is over. " + tempGuess.player1 + " has won with score " 
                        + tempGuess.point1 + " to " + tempGuess.point2 + ".\n");
                    sendMessageByName(tempGuess.player1, "You have won.\n");
                    sendMessageByName(tempGuess.player2, "You have lost.\n");
                    increaseGlobalPointbyName(tempGuess.player2);
                    deleteInGuessList(tempGuess);
                }
                else
                {
                    richTextBox1.AppendText("It has been " + tempGuess.round + " between " + tempGuess.player1 + " and "
                        + tempGuess.player2 + ". Now game is over. " + tempGuess.player2 + " has won with score "
                        + tempGuess.point2 + " to " + tempGuess.point1 + ".\n");
                    sendMessageByName(tempGuess.player2, "You have won.\n");
                    sendMessageByName(tempGuess.player1, "You have lost.\n");
                    increaseGlobalPointbyName(tempGuess.player2);
                    deleteInGuessList(tempGuess);
                }
            }
        }
        //THREAD CHECK FOR WINNER !!!! Function @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        public void checkWinner()
        {
            foreach (Guess tempGuess in guessList)
            {
                if (tempGuess.guess1 != -1 && tempGuess.guess2 != -1 && tempGuess.serversGuess != -1) //every one made a guess
                {
                    if (tempGuess.guess1 == tempGuess.guess2)
                    {
                        richTextBox1.AppendText(tempGuess.player1 + " and " + tempGuess.player2 +
                            " have both guessed "+ tempGuess.guess1 + ". This round is a draw.\n");
                        sendMessageByName(tempGuess.player1, "It is a draw.\n");
                        sendMessageByName(tempGuess.player2, "It is a draw.\n");
                        tempGuess.guess1 = -1;
                        tempGuess.guess2 = -1;
                        tempGuess.serversGuess = -1;
                        tempGuess.round++;
                        checkRound(tempGuess);
                    }
                    else if (Math.Abs(tempGuess.serversGuess - tempGuess.guess1) 
                        > Math.Abs(tempGuess.serversGuess - tempGuess.guess2))
                    {
                        richTextBox1.AppendText(tempGuess.player2 + " have guessed closer to server's guess which is "
                            + tempGuess.serversGuess + ". So this round won by " + tempGuess.player2 +
                            " and lost by " + tempGuess.player1+".\n" );
                        sendMessageByName(tempGuess.player1, "You have lost this round.\n");
                        sendMessageByName(tempGuess.player2, "You have won this round.\n");
                        tempGuess.guess1 = -1;
                        tempGuess.guess2 = -1;
                        tempGuess.serversGuess = -1;
                        tempGuess.point2++;
                        tempGuess.round++;
                        checkRound(tempGuess);
                    }
                    else
                    {
                        richTextBox1.AppendText(tempGuess.player1 + " have guessed closer to server's guess which is "
                           + tempGuess.serversGuess + ". And so this round is won by " + tempGuess.player1 +
                           " and lost by " + tempGuess.player2 + ".\n");
                        sendMessageByName(tempGuess.player2, "You have lost this round.\n");
                        sendMessageByName(tempGuess.player1, "You have won this round.\n");
                        tempGuess.guess1 = -1;
                        tempGuess.guess2 = -1;
                        tempGuess.serversGuess = -1;
                        tempGuess.point1++;
                        tempGuess.round++;
                        checkRound(tempGuess);
                    }
                }
            }
        }
        //When round is over initialze every thing to beinging values
        //When game is over initialze every value
        //Whenever connection drops or one surrender loses the game

        public void guessNumber(Socket theSocket, string message)
        {
            if (message.Length > 5)
            {
                //you have already made guess
                //you are not in a game
                //your guess is not valid
                string tempPlayer = returnUserName(theSocket);
                string tempMessage = message.Substring(0, 5);
                richTextBox1.AppendText(tempMessage + "\n");
                if (tempMessage == "Guess")
                {   if (playerList.Contains(tempPlayer))
                    {

                        string tempNumber = message.Substring(5, message.Length - 5);
                        richTextBox1.AppendText(tempNumber + " temp number\n");
                        int myInt;
                        bool isValid = int.TryParse(tempNumber, out myInt); // the out keyword allows the method to essentially "return" a second value
                        richTextBox1.AppendText(myInt + " myInt number\n");
                        if (isValid)
                        {
                            for (int i = 0; i < GamerList.Count; i++)
                            {                               
                                if (GamerList[i].player1 == tempPlayer)
                                {
                                    
                                    for (int k = 0; k < guessList.Count; i++)
                                    {

                                        if (guessList[k].player1 == tempPlayer)
                                        {
                                            if (guessList[k].serversGuess == -1)
                                            {
                                                guessList[k].serversGuess = makeRandomChoice();
                                            }
                                            if (guessList[k].guess1 != -1)
                                            { //you have already made a guess
                                                richTextBox1.AppendText(tempPlayer + " already made a guess.\n");
                                                sendMessageByName(tempPlayer, "You have already made a guess.\n");
                                            }
                                            else
                                            {
                                                richTextBox1.AppendText(tempPlayer + "'s guess is "+ myInt +".\n");
                                                sendMessageByName(tempPlayer, "You guessed "+ myInt +".\n");
                                                guessList[k].guess1 = myInt;
                                                checkWinner();
                                            }
                                            break;
                                        }
                                    }

                                }
                                if (GamerList[i].player2 == tempPlayer)
                                {
                                    for (int k = 0; k < guessList.Count; i++)
                                    {
                                        if (guessList[k].player1 == tempPlayer)
                                        {
                                            if (guessList[k].serversGuess == -1)
                                            {
                                                guessList[k].serversGuess = makeRandomChoice();
                                            }
                                            if (guessList[k].guess1 != -1)
                                            { //you have already made a guess
                                                richTextBox1.AppendText(tempPlayer + " already made a guess.\n");
                                                sendMessageByName(tempPlayer, "You have already made a guess.\n");
                                            }
                                            else
                                            {
                                                richTextBox1.AppendText(tempPlayer + "'s guess is " + myInt + ".\n");
                                                sendMessageByName(tempPlayer, "You guessed " + myInt + ".\n");
                                                guessList[k].guess1 = myInt;
                                                checkWinner();
                                            }
                                            break;
                                        }
                                    }
                                }                             
                            }
                        }
                        else //not valid integer
                        {
                            sendMessage(theSocket, myInt + " is not a valid integer for the game.\n");
                        }
                    }
                    else // you are not int game
                    {
                        sendMessage(theSocket, "You are not in a game.\n");
                    }
                }
            }
        }
        
        //eger oyundaysa surrender edebilir
        //rakibi bir puan kazanir
        public void surrenderGame(Socket n)
        {
            if(playerList.Contains(returnUserName(n)))
            {
                string temp = returnOpponentName(GamerList, returnUserName(n));
                sendMessage(n, "You have lost the game.\n");
                sendMessageByName(temp, "You have won the game.\n");
                increaseGlobalPointbyName(temp);
                deleteFromGuessListByName(returnUserName(n));
            }
            else
            { //you are not in a game
                sendMessage(n, "You are not in a game.\n");
            }
        }

        public void terminator(Socket n)
        {
            Thread.Sleep(5000);
            removeGamelist(GamerList, playerList, returnUserName(n), " ");
            deleteFromGuessListByName(returnUserName(n));
            deleteFromGlobalPointList(returnUserName(n));
            removeClientList(ClientListStruct, usernameList, n); //disconnected cliet has been remove from userlist
        }

        public void Receive()
        {
            bool connected = true;
            Socket n = socketList[socketList.Count - 1];

            while (connected)
            {
                try
                {
                    Byte[] buffer = new byte[64];
                    int rec = n.Receive(buffer);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));
                    if (newmessage == "TerminateMe")
                    {
                        Thread terminater = new Thread(() => terminator(n));
                        terminater.IsBackground = true;
                        terminater.Start();

                    }
                    if (newmessage == "Give") //"Give" input indicates a cliet is asking for input
                    {
                        string tempUsername = returnUserName(n);
                        richTextBox1.AppendText("Sending player list to Client : " + tempUsername + "\n");
                        sendPlayerList(n);
                    }                    
                    if (newmessage == "Accept")
                    {
                        acceptGame(n);
                    }
                    else if (newmessage == "Reject")
                    {
                        rejectGame(n);
                    }
                    else if (newmessage == "Surrender")
                    {
                        surrenderGame(n);
                    }

                    invitePlayer(n, newmessage);
                    guessNumber(n, newmessage);
                }
                catch
                {
                    if (!terminating) //client has choose to disconnect or a problem occured
                        richTextBox1.AppendText("Client has disconnected : ");
                    //removeGamelist(GamerList, playerList, returnUserName(n), " ");
                    //deleteFromGuessListByName(returnUserName(n));
                    //deleteFromGlobalPointList(returnUserName(n));
                    //removeClientList(ClientListStruct, usernameList, n); //disconnected cliet has been remove from userlist
                    n.Close();                    
                    socketList.Remove(n);
                    connected = false;
                }
            }

        }


        public void Accept()
        {
            while (accept)
            {
                try
                {
                    counter += 1;
                    Socket clientTemp = server.Accept();
                    socketList.Add(clientTemp);
                    if (addClient(clientTemp)) //proceeds if user name is unique
                    {                        
                        Thread thrReceive;
                        thrReceive = new Thread(new ThreadStart(Receive));                       
                        thrReceive.IsBackground = true;
                        thrReceive.Start();
                    }
                    else //removes the socket because username is faulty
                    {
                        sendMessage(clientTemp, "Username is not valid! \n Disconnecting from server...");
                        socketList.Remove(clientTemp);
                    }

                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        richTextBox1.AppendText("Listening socket has stopped working...\n");
                }
            }
        }

        public void sendMessage(Socket clientSocket, string messegeTemp) //sends message to given client
        {
            byte[] bufferTempInitial = Encoding.Default.GetBytes(messegeTemp);
            clientSocket.Send(bufferTempInitial);
        }


        private void listenButton_Click(object sender, EventArgs e)
        {
            int serverPort;
            Thread thrAccept;

            //this port will be used by clients to connect            
            string temp = portInputBox.Text;
            serverPort = Convert.ToInt32(temp);

            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                richTextBox1.AppendText("Started listening for incoming connections.\n");

                server.Listen(10); //the parameter here is maximum length of the pending connections queue
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.IsBackground = true;
                thrAccept.Start();
                listening = true;                
            }
            catch
            {
                terminating = true;
                richTextBox1.AppendText("Cannot create a server with the specified port number. Check the port number and try again.\n");
                richTextBox1.AppendText("terminating...");
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
           richTextBox1.AppendText("Goodbye");
           server.Close();
           terminating = true;
        }
             
        
    }
}

  //string tempMessage = " ";
  //                  if (newmessage.Length > 6)
  //                  {
  //                      tempMessage = newmessage.Substring(0, 6);
  //                  }                    
  //                  if (tempMessage == "Invite")
  //                  {
  //                      int endOfString = newmessage.Length - 6;
  //                      string tempPlayer = newmessage.Substring(6, endOfString);
  //                      string tempUsername = returnUserName(n);
  //                      if (tempUsername != tempPlayer)
  //                      {
  //                          richTextBox1.AppendText(tempUsername + " is asking to invite : " + tempPlayer + ". \n");
  //                          if (usernameList.Contains(tempPlayer))
  //                          {
  //                              if (playerList.Contains(tempPlayer)) //if the person being invited is already playing
  //                              {
  //                                  sendMessage(n, tempPlayer + " is already been playing with someone else. \n");
  //                                  richTextBox1.AppendText(tempPlayer + " is already playing. \n");
  //                              }
  //                              else if (playerList.Contains(tempUsername)) //if person who is inviting is already playing
  //                              {
  //                                  sendMessage(n, "You are already in a game! So you can't invite anybody. \n");
  //                                  richTextBox1.AppendText(tempUsername + " is already playing. \n");
  //                              }
  //                              else //both of them are not playing thus the invitation is sent
  //                              {
  //                                  sendMessageByName(tempPlayer, tempUsername + " is inviting you to play. \n");
  //                                  sendMessage(n, "Your invitation messege has been sent to " + tempPlayer + " .\n");
  //                                  initiateGame(tempUsername, tempPlayer);
  //                              }
  //                          }
  //                          else //if player that is being invited is not exists
  //                          {
  //                              richTextBox1.AppendText(tempUsername + " can't invite " + tempPlayer + " because s/he is not present in lobby. \n");
  //                          }
  //                      }
  //                      else
  //                      {
  //                          sendMessage(n, "You can't accept your own invitation.\n");
  //                          richTextBox1.AppendText(tempUsername + " can't invite itself. \n");
  //                      }

  //                  }
  //                  else if (newmessage == "Surrender")
  //                  {
  //                      string tempUsername = returnUserName(n);
  //                      if (!(playerList.Contains(tempUsername)))
  //                      {
  //                          richTextBox1.AppendText(tempUsername + " is not joined a game. \n");
  //                          sendMessage(n, "You can't surrender. Because you are not in a game. \n");
  //                      }
  //                      else
  //                      {                            
  //                          string tempOpponent = returnOpponentName(GamerList, tempUsername);                        
  //                          sendMessageByName(tempOpponent, "You are PATIENT! You have won! Your opponent : " + tempUsername + " has surrendered. \n");
  //                          sendMessage(n, "You are WEAK! You have lost! Your opponent : " + tempOpponent + " has won. \n");
  //                          //playerList.Remove(tempUsername);
  //                          //playerList.Remove(tempOpponent);
  //                          richTextBox1.AppendText(tempUsername + " has surrendered and " + tempOpponent + " has won. \n");
  //                      }
  //                  }
  //                  else if (newmessage == "Accept")
  //                  {
  //                      string tempUsername = returnUserName(n);
  //                      string temp;
  //                      if (playerListTemp.Contains(tempUsername))
  //                      {
  //                          for (int i = 0; i < GamerListTemp.Count; i++)
  //                          {
  //                              if (GamerListTemp[i].player1 == tempUsername)
  //                              {
  //                                  temp = GamerListTemp[i].player2;                                    
  //                                  sendMessage(n, "You can't accept your own request. \n");
  //                                  richTextBox1.AppendText(tempUsername + " can't accept its own request to start game with " + temp + ". \n");
  //                              }
  //                              else if (GamerListTemp[i].player2 == tempUsername)
  //                              {
  //                                  temp = GamerListTemp[i].player1;
  //                                  playerList.Add(temp);
  //                                  playerList.Add(tempUsername);
  //                                  Games RealGame;
  //                                  RealGame.player1 = temp;
  //                                  RealGame.player2 = tempUsername;
  //                                  GamerList.Add(RealGame);
  //                                  removeGamelist(GamerListTemp, playerListTemp, temp, tempUsername); //remove from temp                           
  //                                  sendMessageByName(temp, "You are in a new game with " + tempUsername + ". \n");
  //                                  sendMessage(n, "You are in a new game with " + temp + ". \n");
  //                                  richTextBox1.AppendText(temp + " and " + tempUsername + " are in a new game. \n");
  //                              }
  //                          }
  //                      }
  //                      else
  //                      {
  //                          richTextBox1.AppendText("There is no request for user: " + tempUsername + " . \n");
  //                          sendMessage(n, "There is no request for you. Thus you can't accept.\n");
  //                      }

  //                  }
  //                  else if (newmessage == "Reject")
  //                  {
  //                      string tempUsername = returnUserName(n);
  //                      string temp;
  //                      if (playerListTemp.Contains(tempUsername))
  //                      {
  //                          for (int i = 0; i < GamerListTemp.Count; i++)
  //                          {   
  //                                  if (GamerListTemp[i].player1 == tempUsername)
  //                                  {
  //                                      temp = GamerListTemp[i].player2;                                    
  //                                      sendMessage(n, "You can't reject your own request. \n");
  //                                      richTextBox1.AppendText(tempUsername + " can't reject its own request to start game with " + temp + ". \n");
  //                                      break;
  //                                  }
  //                                  else if (GamerListTemp[i].player2 == tempUsername)
  //                                  {
  //                                      temp = GamerListTemp[i].player1;
  //                                      removeGamelist(GamerListTemp, playerListTemp, temp, tempUsername); //remove from temp    
  //                                      sendMessageByName(temp, "You have been rejected by " + tempUsername + " .\n");
  //                                      sendMessage(n, "You have rejected player: " + temp + ". \n");
  //                                      richTextBox1.AppendText(tempUsername + " has rejected the request from " + temp + ". \n");
  //                                      break;
  //                                  }
  //                          }
  //                      }
  //                      else
  //                      {
  //                          richTextBox1.AppendText("There is no request for user: " + tempUsername + ". \n");
  //                          sendMessage(n, "You can't reject. Because you are not invited. \n");
  //                      }

  //                  }

