﻿using Pustovoy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustovoy.Domain.Models
{
	public class ResponseData<T>
	{
		// запрашиваемые данные
		public T Data { get; set; }
		// признак успешного завершения запроса
		public bool Success { get; set; } = true;
		// сообщение в случае неуспешного завершения
		public string? ErrorMessage { get; set; }

        public static implicit operator ResponseData<T>(HttpResponseMessage v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator ResponseData<T>(Dish? v)
        {
            throw new NotImplementedException();
        }
    }
}
