using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Queries
{
    public class GetSubmissions : IRequest<GetSubmissionsResponse>
    {
        public int Page { get; set; }
    }
    public class GetSubmissionsHandler : IRequestHandler<GetSubmissions, GetSubmissionsResponse>
    {
        private readonly ICompetitionRepository _competitionRepository;

        public GetSubmissionsHandler(ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public async Task<GetSubmissionsResponse> Handle(GetSubmissions request, CancellationToken cancellationToken = default)
        {

            var response = new GetSubmissionsResponse(request.Page);
            response.Submissions = await _competitionRepository.GetSubmissionsPage(request.Page);
            return response;
        }
    }
    public class GetSubmissionsResponse
    {
        public int Page { get; set; }
        public List<Contestant> Submissions { get; set; } = new List<Contestant>();
        public GetSubmissionsResponse(int page)
        {
            Page = page;
        }
    }
}
