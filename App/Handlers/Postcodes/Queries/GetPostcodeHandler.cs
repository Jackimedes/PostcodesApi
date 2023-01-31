using FluentValidation;
using MediatR;
using PostcodesApi.App.Services.Contracts;

namespace PostcodesApi.App.Handlers.Postcodes.Queries
{
    public class GetPostcodeQuery : IRequest<GetPostcodeQueryVm?>
    {
        public GetPostcodeQuery(string postcode)
        {
            Postcode = postcode;
        }

        public string Postcode { get; set; }
    }

    public class GetPostcodeValidator : AbstractValidator<GetPostcodeQuery>
    {
        public GetPostcodeValidator() 
        {
            RuleFor(r => r.Postcode).NotEmpty();
        }
    }

    public class GetPostcodeHandler : IRequestHandler<GetPostcodeQuery, GetPostcodeQueryVm?>
    {
        private readonly IPostcodesIoApi _postcodesIoApi;

        public GetPostcodeHandler(IPostcodesIoApi postcodesIoApi)
        {
            _postcodesIoApi = postcodesIoApi ?? throw new ArgumentNullException(nameof(postcodesIoApi));
        }

        public async Task<GetPostcodeQueryVm?> Handle(GetPostcodeQuery request, CancellationToken cancellationToken)
        {
            Common.Responses.PostcodesIoApi.GetPostcodeResponse? response = await _postcodesIoApi.Lookup(request.Postcode);

            return GetPostcodeQueryVm.ConvertResponse(response!);
        }
    }
}
