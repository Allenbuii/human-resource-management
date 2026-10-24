using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface ICandidateService : IGenericServiceInterface<Candidate>
    {
        Task<GeneralResponse> SendEmail(Candidate candidate, string baseUrl);
    }
}
