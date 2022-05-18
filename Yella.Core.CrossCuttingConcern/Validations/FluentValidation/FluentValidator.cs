using FluentValidation;
using Yella.Core.Helper.Results;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Yella.Core.CrossCuttingConcern.Validations.FluentValidation;

public static class FluentValidator
{
    public static IDataResult<List<ValidationError>> Validate(object obj, Type validatorType)
    {
        var validator = (IValidator)Activator.CreateInstance(validatorType)!;

        var context = new ValidationContext<object>(obj);

        var validationResult = validator.Validate(context);

        var validationErrors = validationResult.Errors.Distinct().Select(validationFailure => new ValidationError(validationFailure.ErrorMessage, validationFailure.ErrorCode, validationFailure.ErrorMessage)).ToList();

        var list = validationErrors.Select(x => x.Message).Distinct().ToList().Select(variable => new ValidationError("", "", variable)).ToList();

        if (list.Count == 0)
            return new SuccessDataResult<List<ValidationError>>();

        return new ErrorDataResult<List<ValidationError>>(list);

    }


}