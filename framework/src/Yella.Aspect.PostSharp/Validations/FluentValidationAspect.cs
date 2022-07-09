using FluentValidation;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;
using Yella.CrossCuttingConcern.Validations.FluentValidation;
using Yella.Utilities.Extensions;

namespace Yella.Aspect.PostSharp.Validations;


[PSerializable]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
[ProvideAspectRole(StandardRoles.Validation)]
public class FluentValidationAspect : OnMethodBoundaryAspect
{

    public Type Validator;

    public FluentValidationAspect(Type validator) => Validator = validator;

    public override void OnEntry(MethodExecutionArgs args)
    {
        if (args == null) throw new ArgumentNullException(nameof(args));

        var validatorBaseType = Validator.BaseType ?? throw new ArgumentNullException(nameof(Validator.BaseType));

        var entityType = validatorBaseType.GetGenericArguments()[0];

        var entity = args.Arguments.First(t => t.GetType() == entityType);

        var result = FluentValidator.Validate(entity, Validator);

        if (!result.Success)
        {
            throw new ValidationException(result.Data.ToJson());
        }

    }
}