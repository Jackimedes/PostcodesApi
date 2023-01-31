using PostcodesApi.App.Common.Responses.PostcodesIoApi;

namespace PostcodesApi.App.Services.Contracts
{
    public interface IPostcodesIoApi
    {
        public Task<GetPostcodeResponse> Lookup(string postcode);
    }
}