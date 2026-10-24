using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ICandidateInterface : IGenericRepositoryInterface<Candidate>
    {
        Task<GeneralResponse> SendEmail(Candidate candidate);
    }
}
