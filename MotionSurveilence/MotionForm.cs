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
        private Ozeki.Media.DrawingImageProvider _imageProvider;
        private Ozeki.Media.MediaConnector _mediaConnector;
        private MPEG4Recorder _recorder;
        private Ozeki.Media.CameraURLBuilderWF _myCameraUrlBuilder;
        ImageProcesserHandler _imageProcesserHandler;
        IMotionDetector _detector;
        //
        //Constructor for motion form
        //
        public MotionForm()
        {
            InitializeComponent();

            _imageProvider = new Ozeki.Media.DrawingImageProvider();
            _mediaConnector = new Ozeki.Media.MediaConnector();
            var data = new Ozeki.Media.CameraURLBuilderData { DeviceTypeFilter = Ozeki.Media.DiscoverDeviceType.Both };
            _myCameraUrlBuilder = new Ozeki.Media.CameraURLBuilderWF(data);

            _detector = ImageProcesserFactory.CreateMotionDetector();
            _imageProcesserHandler = new ImageProcesserHandler();
            _imageProcesserHandler.AddProcesser(_detector);

            videoViewerWF1.SetImageProvider(_imageProvider);
        }

        private void InvokeGuiThread(Action action)
        {
            BeginInvoke(action);
        }

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
        //Brings up dialog to find camera
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

                _mediaConnector.Connect(_webCamera.VideoChannel, _imageProcesserHandler);
                _mediaConnector.Connect(_imageProcesserHandler, _imageProvider);

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
        //Check box. Shows motion detection when checked and 
        //does not show motion detection when unchecked
        //
        private void chck_box_detect_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_box_detect.Checked)
            {
                _imageProcesserHandler.Start();
            }
            else
            {
                _imageProcesserHandler.Stop();
            }
           
        }

        void _recorder_MultiplexFinished(object sender, VoIPEventArgs<bool> e)
        {
            _recorder.MultiplexFinished -= _recorder_MultiplexFinished;
            _recorder.Dispose();
        }

        private void StartRecording_Click(object sender, EventArgs e)
        {
            if (_webCamera.VideoChannel == null) return;
            var date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                        DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;

            var currentpath = AppDomain.CurrentDomain.BaseDirectory + date + ".mpeg4";

            _recorder = new MPEG4Recorder(currentpath);
            _recorder.MultiplexFinished += _recorder_MultiplexFinished;

            _mediaConnector.Connect(_webCamera.VideoChannel, _recorder.VideoRecorder);
        }

        private void StopRecording_Click(object sender, EventArgs e)
        {
            if (_webCamera.VideoChannel == null) return;
            _mediaConnector.Disconnect(_webCamera.VideoChannel, _recorder.VideoRecorder);
            _recorder.Multiplex();
        }
    }
}
