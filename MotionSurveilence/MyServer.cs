using Ozeki.Camera;
using Ozeki.Media;

namespace MotionSurveilence
{
    public class MyServer : CameraServer
    {
        private MediaConnector _connector;

        private ICameraClient _client;
        //
        //
        //Initializes the media connecter allowing app to act as a streaming server
        public MyServer()
        {
            _connector = new MediaConnector();
        }
        //
        //Connects client to the camera stream
        //
        protected override void OnClientConnected(ICameraClient client)
        {
            _client = client;
            ConnectClientToAudioStream(client);
            ConnectClientToVideoStream(client);

            base.OnClientConnected(_client);
        }
        //
        //Disocnnects the client from the camera stream
        //
        protected override void OnClientDisconnected(ICameraClient client)
        {
            _client = client;

            DisconnectClientFromAudioStream(client);
            DisconnectClientFromVideoStream(client);

            base.OnClientDisconnected(_client);
        }
    }
}
