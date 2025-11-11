public struct ResultMsg
{
    bool result;
    string message;

    public bool Result
    {
        get { return result; }
    }
    public string Msg
    {
        get { return message; }
    }

    public ResultMsg(bool _result, string _msg)
    {
        result = _result;
        message = _msg;
    }

}
