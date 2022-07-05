using ECommerce.Shared.Utilities.ComplexTypes;

namespace ECommerce.Shared.Utilities.Abstract;

public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
