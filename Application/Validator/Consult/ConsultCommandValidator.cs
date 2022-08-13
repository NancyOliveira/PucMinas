using Application.Command.Consult;
using Domain.Constant.Consult;
using FluentValidation;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Application.Validator.Consult
{
    public class ConsultCommandValidator : AbstractValidator<ConsultCommand>
    {
        public ConsultCommandValidator()
        {
            RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(ConsultValidatorConstant.CPF_MANDATORY))
                .Custom((document, context) =>
                {
                    bool isValid = IsValidCpf(document);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(ConsultValidatorConstant.CPF_HAS_INVALID_FORMAT));
                    }
                });
            
            RuleFor(x => x.DateConsult).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(ConsultValidatorConstant.DATECONSULT_MANDATORY))
                .Custom((dateConsult, context) =>
                {
                    bool isValid = IsDateValid(dateConsult);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(ConsultValidatorConstant.DATECONSULT_HAS_INVALID));
                    }
                });
            
            RuleFor(x => x.ServiceID).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(ConsultValidatorConstant.SERVICEID_MANDATORY))
                .Custom((serviceID, context) =>
                {
                    bool isValid = IsServiceIDValid(serviceID);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(ConsultValidatorConstant.SERVICEID_HAS_INVALID));
                    }
                });
        }

        private bool IsValidCpf(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
            {
                return false;
            }

            document = document.Trim();
            document = document.Replace(".", "").Replace("-", "");

            return (document.Length == 11)
                    && !Regex.IsMatch(document, @"(?i)^[a-z]+", RegexOptions.IgnoreCase)
                    && !IsInvalidatedCPF(document)
                    && HasValidDigit(document);
        }

        private bool IsInvalidatedCPF(string document)
        {
            //Cpfs validos INUTILIZADOS pela Receita Federal
            List<string> cpfInvalidated = new List<string>
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            return cpfInvalidated.Contains(document);
        }

        private bool HasValidDigit(string document)
        {
            int[] firstMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = document.Substring(0, 9);
            string initialDigit = CalculateDigit(firstMultiplier, tempCpf);
            tempCpf += initialDigit;

            string finalDigit = CalculateDigit(secondMultiplier, tempCpf);

            string digito = initialDigit + finalDigit;
            return document.EndsWith(digito);
        }

        private static string CalculateDigit(int[] multipliers, string tempCpf)
        {
            int summation = 0;

            for (int i = 0; i < multipliers.Length; i++)
            {
                summation += int.Parse(tempCpf[i].ToString()) * multipliers[i];
            }
            int mod = summation % 11;
            mod = mod < 2 ? 0 : 11 - mod;

            string digit = mod.ToString();
            return digit;
        }

        private bool IsServiceIDValid(int serviceID)
        {
            if(serviceID <= 0)
            {
                return false;
            }

            return true;
        }

        private bool IsDateValid(DateTime dateConsul)
        {
            if(dateConsul > DateTime.Now)
            {
                return true;
            }
            if (dateConsul == DateTime.MinValue)
            {
                return false;
            }

            return true;
        }
    }
}