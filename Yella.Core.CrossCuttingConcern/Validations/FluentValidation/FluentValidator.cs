using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Archseptia.Core.CrossCuttingConcern.Validations.FluentValidation
{
    public static class FluentValidator
    {
        public static void Validate(List<object> entities, Type Validator)
        {

            var validator = (IValidator)Activator.CreateInstance(Validator)!;
            foreach (var validationResult in from entity in entities where validator != null select new ValidationContext<object>(entity) into context select validator.Validate(context) into result select result.Errors.Distinct().Select(validationFailure => new ValidationError(validationFailure.ErrorMessage, validationFailure.ErrorCode, validationFailure.ErrorMessage)).ToList())
            {
                var list = validationResult.Select(x => x.Message).Distinct().ToList().Select(variable => new ValidationError("", "", variable)).ToList();

                if (list.Count == 0)
                    return;

                var json = JsonConvert.SerializeObject(list);
                throw new ValidationException(json);
            }
        }

    }


    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Field { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        public string Message { get; }

        public ValidationError(string? field, string code, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
