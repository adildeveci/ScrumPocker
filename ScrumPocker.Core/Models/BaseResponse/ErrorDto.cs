using System.Collections.Generic;

namespace ScrumPocker.Core.Models.BaseResponse
{
    public class ErrorDto
    {
        public List<string> Messages { get; private set; } = new List<string>();

        #region ctor

        public ErrorDto(string errorMessage)
        {
            Messages.Add(errorMessage);
        }

        public ErrorDto(List<string> errors)
        {
            Messages = errors;
        }

        #endregion

        public override string ToString()
        {
            return string.Join(" ", Messages);
        }
        public string ToString(string sperator)
        {
            return string.Join(sperator, Messages);
        }
    }
}
