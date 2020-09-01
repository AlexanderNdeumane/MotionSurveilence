using Ozeki.Camera;
using System;
using System.Windows.Forms;
using Ozeki.Vision;
using Ozeki.Media;

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
        //
        //Method that initiates the program
        //
        public MotionForm()
        {
            InitializeComponent();

            _motionRecorder = new MotionDetector();
            _motionDetector = new MotionDetector();
            

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

            buttonConnect.Enabled = true;
        }
        //
        //Connects to camera url specified in the camera url text box
        //
        private void buttonConnect_Click(object sender, EventArgs e)
        {
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
            }
        }
        //
        //Disconnects from the camera
        //
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            videoViewerWF1.Stop();
            _webCamera.Stop();
            _mediaConnector.Disconnect(_webCamera.VideoChannel, _imageProvider);
            _webCamera = null;
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
             
        }
        //
        //when record is checked, application streams and records
        //webcam footage
        //
        private void rad_rec_normal_CheckedChanged(object sender, EventArgs e)
        {
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
            _mediaConnector.Connect(_webCamera.VideoChannel, _motionRecorder);
            _mediaConnector.Connect(_motionRecorder, _imageProvider);
            if (rad_rec_OnMotion.Checked)
            {
                _motionRecorder.HighlightMotion = HighlightMotion.Highlight;
                _motionRecorder.MotionColor = MotionColor.Blue;
                _motionRecorder.MotionDetection += _motionRecorder_MotionDetection;
                _motionRecorder.Start();
            }
            else
            {
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
    }
}
