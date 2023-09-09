using Newtonsoft.Json;


namespace Persons_api.Milddleware
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
