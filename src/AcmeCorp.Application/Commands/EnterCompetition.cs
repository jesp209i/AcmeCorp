using AcmeCorp.Infrastructure.Interfaces;
using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Commands
{
    public class EnterCompetition : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SerialNumber { get; set; }
        public bool ConfirmsCorrectAge { get; set; }
        public bool AcceptsTerms { get; set; }
    }
    public class EnterCompetitionValidator : AbstractValidator<EnterCompetition>
    {
        private readonly IProductService _productService;
        private readonly ICompetitionRepository _competitionRepo;

        public EnterCompetitionValidator(IProductService productService, ICompetitionRepository competitionRepo)
        {
            _productService = productService;
            _competitionRepo = competitionRepo;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("You must write you first name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("You must write you last name.");
            RuleFor(x => x.ConfirmsCorrectAge).Equal(true).WithMessage("You must be 18 to enter.");
            RuleFor(x => x.AcceptsTerms).Equal(true).WithMessage("You must accept the terms and condition to enter.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You must provide a valid email address, for us to contact you in the case you win the draw.");
            RuleFor(x => x.SerialNumber)
                .MustAsync(async (x, _) => { return await _productService.IsSerialNumberValid(x); }).WithMessage("You must provide a valid serial number to enter competition.")
                .MustAsync(async (x, _) => { 
                    bool eligibleSerialNumber = await _competitionRepo.IsSerialNumberEligible(x); 
                    return eligibleSerialNumber; 
                }).WithMessage("Serial number is no longer eligiable to enter draw.");
            
        }
    }
    public class EnterCompetitionHandler : IRequestHandler<EnterCompetition>
    {
        private readonly ICompetitionRepository _competitionRepo;

        public EnterCompetitionHandler(ICompetitionRepository competitionRepository)
        {
            _competitionRepo = competitionRepository;
        }
        public async Task<Unit> Handle(EnterCompetition request, CancellationToken cancellationToken = default)
        {
            var contestant = Contestant.Create(request.FirstName, request.LastName, request.Email, request.SerialNumber, request.ConfirmsCorrectAge, request.AcceptsTerms);
            await _competitionRepo.AddContestant(contestant);
            return new Unit();
        }
    }
}
