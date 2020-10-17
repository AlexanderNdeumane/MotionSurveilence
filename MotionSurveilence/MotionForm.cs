using Ozeki.Camera;
using System;
using System.Windows.Forms;
using Ozeki.Vision;
using Ozeki.Media;
using System.Reactive;
using Ozeki.Network;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using System.Threading;
using MQTTnet.Client;

namespace MotionSurveilence
{
    public partial class MotionForm : Form
    {
        private OzekiCamera _webCamera;
        private DrawingImageProvider _imageProvider;
        private MediaConnector _mediaConnector;
        private MPEG4Recorder _recorder;
        private CameraURLBuilderWF _myCameraUrlBuilder;
        private MotionDetector _motionDetector;
        private MotionDetector _motionRecorder;
        private MyServer _server;
        private MqttFactory factory;
        private MqttClient mqttClient;
        private Boolean mqttFlag;
        //private MqttApplicationMessage message;
        //private MqttClientOptionsBuilder options;

        //
        //Method that initiates the program
        //
        public MotionForm()
        {
            InitializeComponent();

            _motionRecorder = new MotionDetector();
            _motionDetector = new MotionDetector();

            mqttFlag = false;

            _imageProvider = new Ozeki.Media.DrawingImageProvider();
            _mediaConnector = new Ozeki.Media.MediaConnector();
            var data = new Ozeki.Media.CameraURLBuilderData { DeviceTypeFilter = Ozeki.Media.DiscoverDeviceType.Both };
            _myCameraUrlBuilder = new Ozeki.Media.CameraURLBuilderWF(data);

            videoViewerWF1.SetImageProvider(_imageProvider);
        }
        //
        //
        //
        private void InvokeGuiThread(Action action)
        {
            BeginInvoke(action);
        }
        //
        //
        //
        void _webCamera_CameraStateChanged(object sender, CameraStateEventArgs e)
        {
            InvokeGuiThread(() =>
            {
                switch (e.State)
                {
                    case CameraState.Streaming:
                        buttonDisconnect.Enabled = true;
                        break;
                    case CameraState.Disconnected:
                        buttonDisconnect.Enabled = false;
                        buttonConnect.Enabled = true;
                        break;
                }
            });

            InvokeGuiThread(() =>
            {
                labelState.Text = e.State.ToString();
            });
        }
        //
        //Brings up dialog to find and connect to camera
        //
        private void buttonCompose_Click(object sender, EventArgs e)
        {
            var result = _myCameraUrlBuilder.ShowDialog();

            if (result != DialogResult.OK)
                return;

            cameraUrl.Text = _myCameraUrlBuilder.CameraURL;
            //enable connect button and disable compose button
            buttonConnect.Enabled = true;
        }
        //
        //Connects to camera url specified in the camera url text box
        //
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(cameraUrl.Text != "")
            {
                if (_webCamera != null)
                {
                    videoViewerWF1.Stop();
                    _webCamera.Stop();
                    _mediaConnector.Disconnect(_webCamera.VideoChannel, _imageProvider);
                }
                buttonConnect.Enabled = false;

                _webCamera = new OzekiCamera(_myCameraUrlBuilder.CameraURL);

                _webCamera.CameraStateChanged += _webCamera_CameraStateChanged;

                _mediaConnector.Connect(_webCamera.VideoChannel, _motionDetector);
                _mediaConnector.Connect(_motionDetector, _imageProvider);

                _webCamera.Start();

                videoViewerWF1.Start();
                buttonCompose.Enabled = false;
                enableButtons();
            }
            else
            {
                //give error
            }
                
            
        }
        //
        //Disconnects from the camera
        //
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if(_webCamera != null)
            {
                videoViewerWF1.Stop();
                _webCamera.Stop();
                _mediaConnector.Disconnect(_webCamera.VideoChannel, _imageProvider);
                _webCamera = null;
                buttonCompose.Enabled = true;
                videoViewerWF1.ClearScreen();
                disableButtons();
            }
        }
        
        //
        //enable buttons when connected to a camera
        //
        public void enableButtons()
        {
            rad_normal.Enabled = true;
            rad_rec_normal.Enabled = true;
            rad_motion.Enabled = true;
            rad_rec_AndDet_motion.Enabled = true;
            rad_rec_OnMotion.Enabled = true;
            buttonStartServer.Enabled = true;
            cb_mobileAlert.Enabled = true;
        }
        //
        //disable buttons when disconnected from a camera
        //
        public void disableButtons()
        {
            rad_normal.Enabled = false;
            rad_rec_normal.Enabled = false;
            rad_motion.Enabled = false;
            rad_rec_AndDet_motion.Enabled = false;
            rad_rec_OnMotion.Enabled = false;
            buttonStartServer.Enabled = false;
            buttonStopServer.Enabled = false;
            cb_mobileAlert.Enabled = false;
        }

        //
        //Recording function
        //
        void _recorder_MultiplexFinished(object sender, VoIPEventArgs<bool> e)
        {
            _recorder.MultiplexFinished -= _recorder_MultiplexFinished;
            _recorder.Dispose();
        }
        //
        //Method that starts video recording
        //
        private void StartRecording()
        {
            if (_webCamera.VideoChannel == null) return;
            var date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                        DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

            var currentpath = AppDomain.CurrentDomain.BaseDirectory + date + ".mpeg4";

            _recorder = new MPEG4Recorder(currentpath);
            _recorder.MultiplexFinished += _recorder_MultiplexFinished;

            _mediaConnector.Connect(_webCamera.VideoChannel, _recorder.VideoRecorder);
        }
        //
        //Method that stops video recording
        //
        private void StopRecording()
        {
            if (_webCamera.VideoChannel == null) return;
            _mediaConnector.Disconnect(_webCamera.VideoChannel, _recorder.VideoRecorder);
            _recorder.Multiplex();
        }
        
        //
        //Method that detects motion in the video
        //
        private void _motionDetector_MotionDetection(object sender, MotionDetectionEvent e)
        {
            switch(e.Detection)
            {
                case true:
                    InvokeGuiThread(() => label_Motion.Text = "Detecting motion");
                    PublishMessageMqtt();
                    break;
                case false:
                    InvokeGuiThread(() => label_Motion.Text = "No motion detected");
                    break;
            }
        }
        //
        //Method detects motion in video for recording
        //
        private void _motionRecorder_MotionDetection(object sender, MotionDetectionEvent e)
        {
            if(e.Detection == true)
            {
                InvokeGuiThread(() => label_Motion.Text = "Motion recorder started");
                StartRecording();
            }
            else
            {
                InvokeGuiThread(() => label_Motion.Text = "Motion recorder stopped");
                StopRecording();
            }

        }
        //
        //When radio normal radio button is checked, application
        //streams webcam footage
        //
        private void rad_normal_CheckedChanged(object sender, EventArgs e)
        {
            uncheck_mobile_alert();
        }
        //
        //when record is checked, application streams and records
        //webcam footage
        //
        private void rad_rec_normal_CheckedChanged(object sender, EventArgs e)
        {
            uncheck_mobile_alert();
            if (rad_rec_normal.Checked)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }
        //
        //when detect motion is checked, application streams
        //webcam footage and detects motion
        //
        private void rad_motion_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_motion.Checked)
            {
                StartMotionDetection();
            }
            else
            {
                StopMotionDetection();
            }
        }
        //
        //when record and detect motion is checked, application
        //streams webcam footage, detects motion and records
        //
        private void rad_rec_AndDet_motion_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_rec_AndDet_motion.Checked)
            {
                StartRecording();
                StartMotionDetection();
            }
            else
            {
                StopRecording();
                StopMotionDetection();
            }
        }
        //
        //when record on motion detection is checked, application
        //streams webcam footage and records when motion is detected
        //
        private void rad_rec_OnMotion_CheckedChanged(object sender, EventArgs e)
        {

            if (rad_rec_OnMotion.Checked)
            {
                _mediaConnector.Connect(_webCamera.VideoChannel, _motionRecorder);
                _mediaConnector.Connect(_motionRecorder, _imageProvider);
                _motionRecorder.HighlightMotion = HighlightMotion.Highlight;
                _motionRecorder.MotionColor = MotionColor.Blue;
                _motionRecorder.MotionDetection += _motionRecorder_MotionDetection;
                _motionRecorder.Start();
            }
            else
            {
                _mediaConnector.Disconnect(_webCamera.VideoChannel, _motionRecorder);
                _mediaConnector.Disconnect(_motionRecorder, _imageProvider);
                _motionRecorder.MotionDetection -= _motionRecorder_MotionDetection;
                _motionRecorder.Stop();
                
            }
        }
        //
        //Method starts motion detection engine
        //
        public void StartMotionDetection()
        {
            _motionDetector.HighlightMotion = HighlightMotion.Highlight;
            _motionDetector.MotionColor = MotionColor.Red;
            _motionDetector.MotionDetection += _motionDetector_MotionDetection;
            _motionDetector.Start();
            label_Motion.Text = "Motion detector started";
        }
        //
        //Method stops motion detection engine
        //
        public void StopMotionDetection()
        {
            _motionDetector.MotionDetection -= _motionDetector_MotionDetection;
            _motionDetector.Stop();
            label_Motion.Text = "Motion detector stopped";
        }
        //
        //method starts the streaming server when
        //start button is clicked
        //
        private void buttonStartServer_Click(object sender, EventArgs e) 
        {
            _server = new MyServer();
            _server.ClientCountChanged += _server_OnClientCountChanged;
            try
            {
                var ip = NetworkAddressHelper.GetLocalIP();

                var port = 554;

                _server.VideoSender = _webCamera.VideoChannel;

                _server.Start();

                var url = "rtsp://" + ip.ToString() + ":" + port;

                var config = new OnvifConfig(8088, ip.ToString(), true, url);

                _server.SetOnvifListenAddress(config);

                _server.SetListenAddress(ip.ToString(), port);

                if (config != null)
                {
                    lbl_hint.Visible = true;
                    lbl_endpoint.Visible = true;
                    lbl_hint.Text = ip.ToString().Substring(8);
                    buttonStartServer.Enabled = false;
                    buttonStopServer.Enabled = true;
                }
            }
            catch
            {
                lbl_hint.Visible = true;
                lbl_hint.Text = "Please connect to your wifi network or close and run the program as an administrator";
            }
            
        }
        //
        //method stops the streaming server when stop button
        //is clicked
        //

        private void buttonStopServer_Click(object sender, EventArgs e)
        {
            var port = 554;

            var ip = NetworkAddressHelper.GetLocalIP();

            var url = "rtsp://" + ip.ToString() + ":" + port;

            var config = new OnvifConfig(8088, ip.ToString(), true, url);

            _server.UnsubscribeOnvifListenAddress(config);
            _server.Stop();

            lbl_endpoint.Visible = false;
            lbl_hint.Visible = false;

            buttonStartServer.Enabled = true;
            buttonStopServer.Enabled = false;
        }
        //
        //
        //
        void _server_ServerStateChanged(object sender, CameraServerStateChangedArgs e)
        {
            InvokeGuiThread(() =>
            {
                lblState.Text = e.State.ToString();
            });
        }
        //
        //
        //
        void _server_OnClientCountChanged(object sender, EventArgs e)
        {
            InvokeGuiThread(() =>
            {
                ConnectedClientsList.Items.Clear();

                foreach (var client in _server.ConnectedClients)
                    ConnectedClientsList.Items.Add("End point: " +
                        client.TransportInfo.RemoteEndPoint);
            });
        }
        //
        //
        //
        private async Task ConnectMqtt()
        {
            var ip = NetworkAddressHelper.GetLocalIP();

            factory = new MqttFactory();
            mqttClient = (MqttClient)factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("MotionSurveillence")
                .WithTcpServer(ip.ToString(), 1883)
                .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);
            mqttFlag = true;


            /*mqttClient.UseDisconnectedHandler(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync((IMqttClientOptions)options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {

                }
            });*/
        }
        //
        //
        //
        private async Task DisconnectMqtt()
        {
            await mqttClient.DisconnectAsync();
            mqttFlag = false;
        }

        //
        //
        //
        private async Task PublishMessageMqtt()
        {
            if (mqttFlag == true)
            {
                var message = new MqttApplicationMessageBuilder()
                .WithTopic("look")
                .WithPayload("motion")
                .WithAtMostOnceQoS()
                .WithRetainFlag(false)
                .Build();

                await mqttClient.PublishAsync(message, CancellationToken.None);
            }
        }
        //
        //
        //
        private void cb_mobileAlert_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rad_motion.Checked || rad_rec_AndDet_motion.Checked || rad_rec_OnMotion.Checked)
            {
                lbl_hint.Visible = false;
                if (cb_mobileAlert.Checked == true)
                {
                    var ip = NetworkAddressHelper.GetLocalIP();
                    lbl_hint.Visible = true;
                    lbl_endpoint.Visible = true;
                    lbl_hint.Text = ip.ToString().Substring(8);
                    ConnectMqtt();
                }
                else
                {
                    DisconnectMqtt();
                }

                
            }
            else
            {
                cb_mobileAlert.Checked = false;
                lbl_hint.Visible = true;
                lbl_hint.Text = "Please select one of the motion detection functions to enable mobile alert";
            }
        }
        //
        //
        //
        public void uncheck_mobile_alert()
        {
            if (cb_mobileAlert.Checked)
            {
                cb_mobileAlert.Checked = false;
                lbl_hint.Visible = false;
            }
        }

    }
}
