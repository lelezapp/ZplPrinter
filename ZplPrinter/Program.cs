using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ZplPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get ZPL Command(s) from file
            string zplFileContent = File.ReadAllText(args[0]);
            // Get ZPL IP and Port from config file
            var appSettings = ConfigurationManager.AppSettings;
            string zplIP = appSettings["ZplIP"];
            int zplPort = Int32.Parse(appSettings["ZplPort"]);
            // Open ZPL connection
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(zplIP, zplPort);
            // Send ZPL file content
            StreamWriter streamWriter = new StreamWriter(tcpClient.GetStream());
            streamWriter.Write(zplFileContent);
            streamWriter.Flush();
            // Close ZPL connection
            streamWriter.Close();
            tcpClient.Close();
        }
    }
}