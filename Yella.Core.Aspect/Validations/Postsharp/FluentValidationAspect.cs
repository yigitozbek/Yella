using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Newtonsoft.Json;
using PostSharp.Aspects.Advices;
using PostSharp.Aspects.Dependencies;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;
using Archseptia.Core.CrossCuttingConcern.Validations.FluentValidation;

namespace Yella.Core.Aspect.Validations.Postsharp
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    [ProvideAspectRole(StandardRoles.Validation)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.Tracing)]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        public Type Validator;

        public FluentValidationAspect(Type validator) => Validator = validator;

        public override void OnEntry(MethodExecutionArgs args)
        {
            
            if (Validator.BaseType is null) return;

            var entityType = Validator.BaseType.GetGenericArguments()[0];
            List<object> entities = args.Arguments.Where(t => t.GetType() == entityType).ToList();
                
            FluentValidator.Validate(entities, Validator);

        }
    }
}
