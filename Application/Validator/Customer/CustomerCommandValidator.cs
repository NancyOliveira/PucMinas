using Application.Command.Customer;
using Domain.Constant.Customer;
using FluentValidation;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Application.Validator.Customer
{
    public class CustomerCommandValidator : AbstractValidator<CustomerCommand>
    {
        public CustomerCommandValidator()
        {
            RuleFor(x => x.Document).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.CPF_MANDATORY))
                .Custom((document, context) =>
                {
                    bool isValid = IsValidCpf(document);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.CPF_HAS_INVALID_FORMAT));
                    }
                });

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.NAME_MANDATORY))
                .Custom((name, context) =>
                {
                    bool isValid = IsNameValid(name);
                    if (isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.NAME_HAS_INVALID));
                    }
                });

            RuleFor(x => x.Birthdate).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.BIRTHDATE_MANDATORY))
                .Custom((birthdate, context) =>
                {
                    bool isValid = IsValidBirthdate(birthdate);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.BIRTHDATE_HAS_INVALID));
                    }
                });

            RuleFor(x => x.Adress).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.ADRESS_MANDATORY))
                .Custom((adress, context) =>
                {
                    bool isValid = IsAdressValid(adress);
                    if (isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.ADRESS_HAS_INVALID));
                    }
                });

            RuleFor(x => x.NumberAdress).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.NUMBERADRESS_MANDATORY));

            RuleFor(x => x.CEP).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.CEP_MANDATORY)).Custom((cep, context) =>
               {
                   bool isValid = IsValidCep(cep);
                   if (isValid)
                   {
                       context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.CEP_HAS_INVALID));
                   }
               });

            RuleFor(x => x.DDD).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.DDD_MANDATORY))
                .Custom((ddd, context) =>
                {
                    bool isValid = IsValidDDD(ddd);
                    if (isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.DDD_HAS_INVALID));
                    }
                });

            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(CustomerValidatorConstant.PHONE_MANDATORY))
                .Custom((phone, context) =>
                {
                    bool isValid = IsValidPhone(phone);
                    if (isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(CustomerValidatorConstant.PHONE_HAS_INVALID));
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

        //private bool IsDateValid(DateTime dateConsul)
        //{
        //    var date = (DateTime?)dateConsul;
        //    return dateConsul.Date > DateTime.Now;
        //}

        private bool IsNameValid(string name)
        {
            if (name.Any(char.IsDigit))
            {
                return true;
            }

            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(name))
            {
                return true;
            }

            return false;
        }

        private bool IsAdressValid(string name)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(name))
            {
                return true;
            }

            return false;
        }

        private bool IsValidBirthdate(DateTime birthdate)
        {
            bool isValidDate = DateTime.TryParseExact(birthdate.ToString("yyyy-MM-dd"), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture,
                                                    System.Globalization.DateTimeStyles.AssumeUniversal, out DateTime date);

            return isValidDate && (date != default) && (date.Date < DateTime.Today) && (date.Year >= 1820);
        }

        private bool IsValidCep(int cep)
        {
            if (cep.ToString().Length != 7)
            {
                return true;
            }

            List<int> ceps = new List<int>
            {
                00000000,
                11111111,
                22222222,
                33333333,
                44444444,
                55555555,
                66666666,
                77777777,
                88888888,
                99999999
            };

            return ceps.Contains(cep);
        }

        private bool IsValidDDD(int ddd)
        {
            if (ddd.ToString().Length > 3 || ddd.ToString().Length < 2)
            {
                return true;
            }

            List<int> ddds = new List<int>
            {
                11, // São Paulo – SP
                12, // São José dos Campos – SP
                13, // Santos – SP
                14, // Bauru – SP
                15, // Sorocaba – SP
                16, // Ribeirão Preto – SP
                17, // São José do Rio Preto – SP
                18, // Presidente Prudente – SP
                19, // Campinas – SP
                21, // Rio de Janeiro – RJ
                22, // Campos dos Goytacazes – RJ
                24, // Volta Redonda – RJ
                27, // Vila Velha/Vitória – ES
                28, // Cachoeiro de Itapemirim – ES
                31, // Belo Horizonte – MG
                32, // Juiz de Fora – MG
                33, // Governador Valadares – MG
                34, // Uberlândia – MG
                35, // Poços de Caldas – MG
                37, // Divinópolis – MG
                38, // Montes Claros – MG
                41, // Curitiba – PR
                42, // Ponta Grossa – PR
                43, // Londrina – PR
                44, // Maringá – PR
                45, // Foz do Iguaçú – PR
                46, // Francisco Beltrão/Pato Branco – PR
                47, // Joinville – SC
                48, // Florianópolis – SC
                49, // Chapecó – SC
                51, // Porto Alegre – RS
                53, // Pelotas – RS
                54, // Caxias do Sul – RS
                55, // Santa Maria – RS
                61, // Brasília – DF
                62, // Goiânia – GO
                63, // Palmas – TO
                64, // Rio Verde – GO
                65, // Cuiabá – MT
                66, // Rondonópolis – MT
                67, // Campo Grande – MS
                68, // Rio Branco – AC
                69, // Porto Velho – RO
                71, // Salvador – BA
                73, // Ilhéus – BA
                74, // Juazeiro – BA
                75, // Feira de Santana – BA
                77, // Barreiras – BA
                79, // Aracaju – SE
                81, // Recife – PE
                82, // Maceió – AL
                83, // João Pessoa – PB
                84, // Natal – RN
                85, // Fortaleza – CE
                86, // Teresina – PI
                87, // Petrolina – PE
                88, // Juazeiro do Norte – CE
                89, // Picos – PI
                91, // Belém – PA
                92, // Manaus – AM
                93, // Santarém – PA
                94, // Marabá – PA
                95, // Boa Vista – RR
                96, // Macapá – AP
                97, // Coari – AM
                98, // São Luís – MA
                99, // Imperatriz – MA

                011, // São Paulo – SP
                012, // São José dos Campos – SP
                013, // Santos – SP
                014, // Bauru – SP
                015, // Sorocaba – SP
                016, // Ribeirão Preto – SP
                017, // São José do Rio Preto – SP
                018, // Presidente Prudente – SP
                019, // Campinas – SP
                021, // Rio de Janeiro – RJ
                022, // Campos dos Goytacazes – RJ
                024, // Volta Redonda – RJ
                027, // Vila Velha/Vitória – ES
                028, // Cachoeiro de Itapemirim – ES
                031, // Belo Horizonte – MG
                032, // Juiz de Fora – MG
                033, // Governador Valadares – MG
                034, // Uberlândia – MG
                035, // Poços de Caldas – MG
                037, // Divinópolis – MG
                038, // Montes Claros – MG
                041, // Curitiba – PR
                042, // Ponta Grossa – PR
                043, // Londrina – PR
                044, // Maringá – PR
                045, // Foz do Iguaçú – PR
                046, // Francisco Beltrão/Pato Branco – PR
                047, // Joinville – SC
                048, // Florianópolis – SC
                049, // Chapecó – SC
                051, // Porto Alegre – RS
                053, // Pelotas – RS
                054, // Caxias do Sul – RS
                055, // Santa Maria – RS
                061, // Brasília – DF
                062, // Goiânia – GO
                063, // Palmas – TO
                064, // Rio Verde – GO
                065, // Cuiabá – MT
                066, // Rondonópolis – MT
                067, // Campo Grande – MS
                068, // Rio Branco – AC
                069, // Porto Velho – RO
                071, // Salvador – BA
                073, // Ilhéus – BA
                074, // Juazeiro – BA
                075, // Feira de Santana – BA
                077, // Barreiras – BA
                079, // Aracaju – SE
                081, // Recife – PE
                082, // Maceió – AL
                083, // João Pessoa – PB
                084, // Natal – RN
                085, // Fortaleza – CE
                086, // Teresina – PI
                087, // Petrolina – PE
                088, // Juazeiro do Norte – CE
                089, // Picos – PI
                091, // Belém – PA
                092, // Manaus – AM
                093, // Santarém – PA
                094, // Marabá – PA
                095, // Boa Vista – RR
                096, // Macapá – AP
                097, // Coari – AM
                098, // São Luís – MA
                099, // Imperatriz – MA
            };

            return !ddds.Contains(ddd);
        }

        private bool IsValidPhone(int phone)
        {
            if (phone.ToString().Length > 9)
            {
                return true;
            }

            List<int> phones = new List<int>
            {
                000000000,
                111111111,
                222222222,
                333333333,
                444444444,
                555555555,
                666666666,
                777777777,
                888888888,
                999999999
            };

            return phones.Contains(phone);
        }
    }
}
