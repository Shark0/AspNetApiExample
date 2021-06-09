namespace WebApplication1.Controllers.Pojo
{
    public class ResponseDTO<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        
        public T data { get; set; }
    }
}