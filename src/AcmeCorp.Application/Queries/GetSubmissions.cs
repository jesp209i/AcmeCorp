using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Queries
{
    public class GetSubmissions : IRequest<GetSubmissionsResponse>
    {
        public int Page { get; set; }
    }
    public class GetSubmissionsValidator : AbstractValidator<GetSubmissions>
    {
        public GetSubmissionsValidator()
        {
            RuleFor(x => x.Page).Must(x => 0 < x && x < 11).WithMessage("Select a page between 1 and 10.");
        }
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
            int maxPage = await _competitionRepository.MaxPageCount();
            var contestants = await _competitionRepository.GetSubmissionsPage(request.Page);
            var response = new GetSubmissionsResponse(request.Page, maxPage);
            response.Submissions = contestants.Select(x => ContestantVM.Create(x)).ToList();
            return response;
        }
    }
    public class GetSubmissionsResponse
    {
        public int Page { get; set; }
        public int MaxPage { get; set; }
        public List<ContestantVM> Submissions { get; set; } = new List<ContestantVM>();
        public GetSubmissionsResponse(int page, int maxPage)
        {
            Page = page;
            MaxPage = maxPage;
        }
    }
    public class ContestantVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SerialNumber { get; set; }
        public bool ConfirmedAgeRequirement { get; set; }
        public bool AcceptedTerms { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int Entries { get; set; } = 2;
        public static ContestantVM Create(Contestant contestant)
        => new ContestantVM
        {
            Id = contestant.Id,
            FirstName = contestant.FirstName,
            LastName = contestant.LastName,
            Email = contestant.Email,
            SerialNumber = contestant.SerialNumber,
            ConfirmedAgeRequirement = contestant.ConfirmedAgeRequirement,
            AcceptedTerms = contestant.AcceptedTerms,
            CreatedAt = contestant.CreatedAt
        };
    }
}
