using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API.Yandex
{
	public class YandexException : Exception
	{
		public YandexException()
			: base()
		{

		}

		public YandexException(string message)
			: base(message)
		{

		}

		protected YandexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{

		}

		public YandexException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}
}
