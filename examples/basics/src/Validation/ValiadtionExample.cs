using System.Linq;
using TinyFp;
using TinyFp.DataTypes;
using static TinyFp.Prelude;

namespace TinyFpTest.Examples.Basics.Validation
{
    public class ValiadtionExample
    {
        private const string INVALID_MAIL = "invalid mail";
        private const string INVALID_PASSWORD = "invalid password";
        private const string INVALID_CODE = "invalid code";

        static Validation<string, Unit> ValidateMail(string email) 
            => email.Contains("@") ?
                Success<string, Unit>(Unit.Default) :
                Fail<string, Unit>(INVALID_MAIL);

        static Validation<string, Unit> ValidatePassword(string password) 
            => password.Length > 3 ?
                Success<string, Unit>(Unit.Default) :
                Fail<string, Unit>(INVALID_PASSWORD);

        static Validation<string, Unit> ValidateCode(string code) 
            => code.All(char.IsDigit) ?
                Success<string, Unit>(Unit.Default) :
                Fail<string, Unit>(INVALID_CODE);

        public string Validate(string email, string password, string code)
            => ValidateMail(email)
                .Bind(_ => ValidatePassword(password))
                .Bind(_ => ValidateCode(code))
                .Match(
                    _ => string.Empty,
                    _ => _
                );        
    }
}
