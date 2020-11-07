using AcmeCorp.Infrastructure;
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

        public EnterCompetitionValidator(IProductService productService)
        {
            _productService = productService;
            RuleFor(x => x.ConfirmsCorrectAge).Equal(true).WithMessage("You must be 18 to enter.");
            RuleFor(x => x.AcceptsTerms).Equal(true).WithMessage("You must accept the terms and condition to enter.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You must provide a valid email address, for us to contact you in the case you win the draw.");
            RuleFor(x => x.SerialNumber)
                .Must(_productService.IsSerialNumberValid).WithMessage("You must provide a valid serial number to enter competition.")
                .Must(x=> false).WithMessage("Has Been Claimed.");
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
