﻿using ArduinoRecognizeSystems2.Model;
using ArduinoRecognizeSystems2.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArduinoRecognizeSystems
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {   
        
        public MainPage()
        {
            InitializeComponent();
            if (Usuario.GetLocalUser() == null)
            {
                //Application.Current.MainPage = new InicioPage();
                Navigation.PushAsync(new InicioPage());
            } 
        }

        [Obsolete]
        private async void settingbtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Configuracion());
        }
        private void sendData()
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress iPAddress = IPAddress.Parse("192.168.0.112");
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8888);

                string msg = "1_1_1";
                byte[] sendBuffer = Encoding.ASCII.GetBytes(msg);
                socket.SendTo(sendBuffer, iPEndPoint);

                byte[] recBuffer = new byte[1024];
                int bytesrec = socket.Receive(recBuffer);
                TestLabel.Text = Encoding.UTF8.GetString(recBuffer, 0, bytesrec);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }

        private void SendButtom_Clicked(object sender, EventArgs e)
        {
            sendData();
        }

        private async void btLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }
    }
}
