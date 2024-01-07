namespace WebSPA.ReactJs.ApiServices.Interface
{
    public interface IAuthApiService
    {
        Task<string> GetToken();
    }
}
