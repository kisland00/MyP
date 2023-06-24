using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Runtime.Remoting.Contexts;
using Newtonsoft.Json;

namespace BankServer
{
    [Serializable]
    class IOPacket
    {
        public char IO = new char();
        public string CMD = "";
    }
    [Serializable]
    class ReaderPacket
    {
        public List<string> READER;
    }

    internal class Program
    {
        static Socket serverSocket;
        static IPEndPoint ipep;

        static Thread threadAccept;
        static bool isRunAccept = true;

        static IList<Socket> clientList = new List<Socket>();
        static object keyObj = new object();

        static OracleConnection conn = null;
        static private string dbConnInfo = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                    (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = market; Password = market;";
        static private void CreateUser()
        {
            string sysConnInfo = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                    (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = system; Password = 123456;";

            OracleConnection connsys = new OracleConnection(sysConnInfo);
            try
            {
                connsys.Open();    // 연결을 시도한다.
                // 정상적으로 연결이 되었다면
                if (connsys.State == ConnectionState.Open)
                {
                    Console.WriteLine("Oracle Server 연결 성공");
                }
            }
            catch (Exception ex)
            {
                connsys = null;
                Console.WriteLine("DB system Error : " + ex.Message);
            }
            OracleTransaction transaction;
            OracleCommand command = connsys.CreateCommand();
            // Start a local transaction
            transaction = connsys.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            command.Transaction = transaction;

            try
            {
                command.CommandText =
                    "DROP USER market CASCADE";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Drop Error : " + ex.Message);
            }
            try
            {
                command.CommandText =
                     "CREATE USER market IDENTIFIED BY market DEFAULT TABLESPACE users TEMPORARY TABLESPACE temp PROFILE DEFAULT";
                command.ExecuteNonQuery();
                command.CommandText = "GRANT CONNECT, RESOURCE TO market";
                command.ExecuteNonQuery();
                command.CommandText = "GRANT CREATE VIEW, CREATE SYNONYM TO market";
                command.ExecuteNonQuery();
                command.CommandText = "ALTER USER market ACCOUNT UNLOCK";
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("market user Created");
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e.ToString());
                Console.WriteLine("market user Created ERROR");
            }
            command.Dispose();
            transaction.Dispose();
            connsys.Close();
        }
        static private void CreateTable()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            try
            {
                cmd.CommandText = "DROP TABLE MarketTrade";
                OracleDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB DROP MarketTrade Error : " + ex.Message);
            }

            try
            {
                cmd.CommandText = "CREATE TABLE MarketTrade(markId VARCHAR2(50), itemId VARCHAR2(50), seller VARCHAR2(50), count VARCHAR2(50), price VARCHAR2(200), tTime DATE DEFAULT sysdate)";
                OracleDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Create MarketTrade Error : " + ex.Message);
            }

            try
            {
                cmd.CommandText = "DROP TABLE Account";
                OracleDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB DROP Account Error : " + ex.Message);
            }

            try
            {
                cmd.CommandText = "CREATE TABLE Account(usid VARCHAR2(50), pwd VARCHAR2(50), umoney VARCHAR2(50) DEFAULT '20')";
                OracleDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Create Account Error : " + ex.Message);
            }

            cmd.Dispose();
        }

        static private void Connect()
        {
            conn = new OracleConnection(dbConnInfo);
            try
            {
                conn.Open();    // 연결을 시도한다.
                // 정상적으로 연결이 되었다면
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("Oracle Server 연결 성공");
                }
            }
            catch (Exception ex)
            {
                conn = null;
                Console.WriteLine("DB connect Error : " + ex.Message);
            }
        }
        static void ThreadRecv(object socket)
        {
            Socket connSocket = (Socket)socket;
            NetworkStream ns = new NetworkStream(connSocket);
            StreamReader sr = new StreamReader(ns);
            bool isRunRecv = true;
            while (isRunRecv)
            {
                // 데이터 수신
                string data = "";
                try
                {
                    data = sr.ReadLine();
                    if (data == null)
                    {
                        isRunRecv = false;
                        break;
                    }
                    Console.WriteLine($"==> 수신 : {data}");
                    IOPacket inOut = JsonConvert.DeserializeObject<IOPacket>(data);

                    if (inOut.IO == 'I')
                    {
                        lock (keyObj)
                        {
                            InputToDB(inOut.CMD);
                        }
                    }
                    else if (inOut.IO == 'O')
                    {
                        lock (keyObj)
                        {
                            ReturnReader(connSocket, inOut.CMD);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Recv Error : {ex.Message}");
                    isRunRecv = false;
                    break;
                }
            }

            Console.WriteLine($"{connSocket} 클라이언트 접속 종료");

            lock (keyObj)
            {
                clientList.Remove(connSocket);
            }
        }
        private static void InputToDB(string cmdString)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = cmdString;
                    int nRow = cmd.ExecuteNonQuery();   // 실행 후 적용된 행의 수를 반환
                    Console.WriteLine($"INSERT/UPDATE {nRow} 행이 실행됨");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB Error : " + ex.Message);
            }
        }

        private static void ReturnReader(Socket connSocket, string cmdString)
        {
            NetworkStream ns = new NetworkStream(connSocket);
            StreamWriter sw = new StreamWriter(ns);

            List<string> lines = new List<string>();

            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdString;

                OracleDataReader reader = cmd.ExecuteReader();
                string result = "";
                while (reader.Read())
                {
                    result = "";
                    int i = 0;
                    for (i = 0; i < reader.FieldCount - 1; i++)
                    {
                        result += reader.GetValue(i).ToString();
                        result += " ";
                    }
                    result += reader.GetValue(i).ToString();
                    lines.Add(result);
                }
                if (lines.Count == 0)
                    lines.Add("null");
            }
            ReaderPacket sendPacket = new ReaderPacket();
            sendPacket.READER = lines;
            sw.WriteLine(JsonConvert.SerializeObject(sendPacket));
            sw.Flush();
        }

        static void ThreadAccept()
        {
            while (isRunAccept)
            {
                try
                {
                    Console.WriteLine("클라이언트 접속 대기...");
                    Socket connSocket = serverSocket.Accept();
                    Console.WriteLine("클라이언트 접속 연결!!!");

                    // 새로 연결된 소켓을 클라이언트 소켓 리스트에 등록한다.
                    lock (keyObj)
                    {
                        clientList.Add(connSocket);
                    }

                    // 새로운 클라이언트가 접속하면 담당 스레드를 만들어서
                    // 입출력 처리하도록 한다
                    Thread threadRecv = new Thread(new ParameterizedThreadStart(ThreadRecv));
                    threadRecv.Start(connSocket);

                }
                catch (Exception ex)
                {
                    isRunAccept = false;
                    Console.WriteLine("Accept : " + ex.Message);
                }
            }
        }
        static void Main(string[] args)
        {
            CreateUser();
            Connect();
            CreateTable();
            serverSocket = new Socket(AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp);
            ipep = new IPEndPoint(IPAddress.Any, 666);
            serverSocket.Bind(ipep);
            serverSocket.Listen(100);

            Console.WriteLine("서버 시작");

            threadAccept = new Thread(new ThreadStart(ThreadAccept));
            threadAccept.Start();
            threadAccept.Join();
        }
    }
}
