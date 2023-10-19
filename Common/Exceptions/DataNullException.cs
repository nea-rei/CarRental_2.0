namespace Common.Exceptions
{
    public class DataNullException : ArgumentNullException
    {
        public DataNullException(string message) : base(message) { }
    }

    public class DataException : Exception
    {
        public DataException(string message) : base(message) { }
    }
}
