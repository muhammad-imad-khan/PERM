using System.Text.Json;

namespace Perm.Core.ExceptionManager
{
    public class ErrorDetails
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}