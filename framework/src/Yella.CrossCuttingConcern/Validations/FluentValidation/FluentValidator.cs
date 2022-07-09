using FluentValidation;
using Yella.Utilities.Results;

namespace Yella.CrossCuttingConcern.Validations.FluentValidation;

/// <summary>
/// this class uses the Fluent Validation library.
/// </summary>
public class FluentValidator
{
    /// <summary>
    /// This method uses for entity validation. It takes an entity and a Fluent Validation object inside
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="validatorType"></param>
    /// <returns></returns>
    public static IDataResult<List<ValidationError>> Validate(object obj, Type validatorType)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        if (validatorType == null) throw new ArgumentNullException(nameof(validatorType));

        var validator = (IValidator)Activator.CreateInstance(validatorType)!;

        var context = new ValidationContext<object>(obj);

        var validationResult = validator.Validate(context);

        var validationErrors = validationResult.Errors.Distinct().Select(validationFailure => new ValidationError(validationFailure.ErrorMessage, validationFailure.ErrorCode, validationFailure.ErrorMessage)).ToList();

        var list = validationErrors.Select(x => x.Message).Distinct().ToList().Select(variable => new ValidationError("", "", variable)).ToList();

        if (list.Count != 0)
        {
            return new ErrorDataResult<List<ValidationError>>(list);
        }

        return new SuccessDataResult<List<ValidationError>>();

    }
}