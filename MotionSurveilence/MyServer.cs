using Ozeki.Camera;
using Ozeki.Media;

namespace MotionSurveilence
{
    public class MyServer : CameraServer
    {
        private MediaConnector _connector;

        private ICameraClient _client;

        public MyServer()
        {
            _connector = new MediaConnector();
        }

        protected override void OnClientConnected(ICameraClient client)
        {
            _client = client;
            ConnectClientToAudioStream(client);
            ConnectClientToVideoStream(client);

            base.OnClientConnected(_client);
        }

        protected override void OnClientDisconnected(ICameraClient client)
        {
            _client = client;

            DisconnectClientFromAudioStream(client);
            DisconnectClientFromVideoStream(client);

            base.OnClientDisconnected(_client);
        }
    }
}
