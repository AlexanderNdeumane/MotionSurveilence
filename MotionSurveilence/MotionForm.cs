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
using System.IO;

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
        private MyServer _server;
        private MqttFactory factory;
        private MqttClient mqttClient;
        private Boolean mqttFlag;

        //
        //Method that initiates the program
        //
        public MotionForm()
        {
            InitializeComponent();
            _motionDetector = new MotionDetector();

            mqttFlag = false;

            _imageProvider = new Ozeki.Media.DrawingImageProvider();
            _mediaConnector = new Ozeki.Media.MediaConnector();
            var data = new Ozeki.Media.CameraURLBuilderData { DeviceTypeFilter = Ozeki.Media.DiscoverDeviceType.Both };
            _myCameraUrlBuilder = new Ozeki.Media.CameraURLBuilderWF(data);

            videoViewerWF1.SetImageProvider(_imageProvider);
        }
        //
        //Starts the gui of the app
        //
        private void InvokeGuiThread(Action action)
        {
            BeginInvoke(action);
        }
        //
        //Changes the label of the state depending on what is going on
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
            //rad_normal.Enabled = true;
            //rad_rec_normal.Enabled = true;
            //rad_motion.Enabled = true;
            //rad_rec_AndDet_motion.Enabled = true;
            //rad_rec_OnMotion.Enabled = true;
            buttonStartServer.Enabled = true;
            cb_mobileAlert.Enabled = true;
            detectMotion.Enabled = true;
            Start_Recording.Enabled = true;
        }
        //
        //disable buttons when disconnected from a camera
        //
        public void disableButtons()
        {
            //rad_normal.Enabled = false;
            //rad_rec_normal.Enabled = false;
            //rad_motion.Enabled = false;
            //rad_rec_AndDet_motion.Enabled = false;
            //rad_rec_OnMotion.Enabled = false;
            buttonStartServer.Enabled = false;
            buttonStopServer.Enabled = false;
            cb_mobileAlert.Enabled = false;
            detectMotion.Enabled = false;
            Start_Recording.Enabled = false;
            Stop_Recording.Enabled = false;
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
        //Method connects to mosquitto mqtt server
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
        //Method disconnects from the mosquitto mqtt server
        //
        private async Task DisconnectMqtt()
        {
            await mqttClient.DisconnectAsync();
            mqttFlag = false;
        }
        //
        //method publishes a message to the mqtt server when it detects motion
        //allowing clients connected to the server to get notifications
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
        //method changes function of the app depending on the checked
        //status of the "mobile alert" check box.
        //if the check box is checked, it shows the connect number
        //and enables the alert on detection function. If unchecked it
        //disconnects if connected
        //
        private void cb_mobileAlert_CheckedChanged(object sender, EventArgs e)
        {
            
            if (detectMotion.Checked)
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
                lbl_hint.Text = "Please enable motion detection to enable mobile alert";
            }
        }
        //
        //Method starts recording webcam footage when button is pressed
        //
        private void Start_Recording_Click(object sender, EventArgs e)
        {
            if (_webCamera.VideoChannel == null) return;
            var date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                        DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

            var currentpath = @"Recordings\" + date + ".mpeg4";
            if (!Directory.Exists("Recordings"))
            {
                System.IO.Directory.CreateDirectory("Recordings");
            }

            _recorder = new MPEG4Recorder(currentpath);
            _recorder.MultiplexFinished += _recorder_MultiplexFinished;

            _mediaConnector.Connect(_webCamera.VideoChannel, _recorder.VideoRecorder);

            Start_Recording.Enabled = false;
            Stop_Recording.Enabled = true;
        }
        //
        //Method stops recording webcam footage when button is pressed
        //
        private void Stop_Recording_Click(object sender, EventArgs e)
        {
            if (_webCamera.VideoChannel == null) return;
            _mediaConnector.Disconnect(_webCamera.VideoChannel, _recorder.VideoRecorder);
            _recorder.Multiplex();

            Start_Recording.Enabled = true;
            Stop_Recording.Enabled = false;
        }
        //
        //Method enables or disables motion detection depending on the checked status
        //
        private void detectMotion_CheckedChanged(object sender, EventArgs e)
        {
            if (detectMotion.Checked == true)
            {
                StartMotionDetection();
            }
            else
            {
                StopMotionDetection();
                if (cb_mobileAlert.Checked)
                {
                    cb_mobileAlert.Checked = false;
                    lbl_hint.Text = "";
                }
            }
        }
    }
}
