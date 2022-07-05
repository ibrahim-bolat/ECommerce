﻿namespace ECommerce.Shared.Utilities.Abstract;

public interface IDataResult<out T> : IResult
    {
        public T Data { get; }
    }
