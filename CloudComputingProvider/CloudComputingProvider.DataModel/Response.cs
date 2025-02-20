namespace CloudComputingProvider.DataModel
{
    public class Response
    {
        public Response()
        {

        }

        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
        public List<string> ResponseMessages { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
        public Response()
        {
            Data = default(T);
        }
    }
}
