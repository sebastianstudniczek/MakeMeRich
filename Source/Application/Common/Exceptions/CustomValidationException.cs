﻿using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace MakeMeRich.Application.Common.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException()
            :base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            :this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                string propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get;}
    }
}
