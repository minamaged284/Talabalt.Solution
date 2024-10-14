namespace Talabalt.APIS.Errors
{
    public class ApiValidationResponse:ApiResponse
    {
        public IEnumerable<string> Error { get; set; }

        public ApiValidationResponse():base(400)
        {
            
        }
    }
}
