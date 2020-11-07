using AcmeCorp.Infrastructure;
using AcmeCorp.Persistance;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Commands
{
    public class EnterCompetition : IRequest<EnterCompetitionResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ConfirmsCorrectAge { get; set; }
        public string SerialNumber { get; set; }
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
            RuleFor(x => x.ConfirmsCorrectAge).Equal(true).WithMessage("You must be 18 to enter.");
            RuleFor(x => x.AcceptsTerms).Equal(true).WithMessage("You must accept the terms and condition to enter.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You must provide a valid email address, for us to contact you in the case you win the draw.");
            RuleFor(x => x.SerialNumber)
                .Must(_productService.IsSerialNumberValid).WithMessage("You must provide a valid serial number to enter competition.")
                .MustAsync(async (x, _) => { 
                    bool eligibleSerialNumber = await _competitionRepo.IsSerialNumberEligible(x); 
                    return eligibleSerialNumber; 
                }).WithMessage("Serial number is no longer eligiable to enter draw.");
        }
    }
    public class EnterCompetitionHandler : IRequestHandler<EnterCompetition, EnterCompetitionResponse>
    {
        public Task<EnterCompetitionResponse> Handle(EnterCompetition request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class EnterCompetitionResponse
    {
        public bool EntryAccepted { get; set; }
    }
}
