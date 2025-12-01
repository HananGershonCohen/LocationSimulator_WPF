using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace LocationSimulator_WPF
{
    public class UdpSender
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;
        public UdpSender(IPAddress iPAddress, int port)
        {
            _endPoint = new IPEndPoint(iPAddress, port);
            _udpClient = new UdpClient();
        }

        /// <summary>
        /// miss  : handle exception.
        /// </summary>
        public async Task Send(byte[] datagram)
        {
            try
            {
                await _udpClient.SendAsync(datagram, _endPoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UDP Send Error: {ex.Message}");
            }
        }

        public void Close()
        {
            _udpClient.Close();
        }


    }
}
