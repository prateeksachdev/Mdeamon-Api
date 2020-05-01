
namespace AltnCrossAPI.Helper
{
    public class Result
    {
        private bool _Status;
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private object _Data;

        public object Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
    }
}
