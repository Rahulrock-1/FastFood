using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace FastFood.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvaildCredential => Error.Validation(
                code: "Auth.InvaildCredential",
                description: "Invaild Credntial.");
        }
    }
}