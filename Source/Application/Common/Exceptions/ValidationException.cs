using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace MakeMeRich.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            :base("One or more validation failures have occured.")
        {

        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            :this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get;}
    }
}
