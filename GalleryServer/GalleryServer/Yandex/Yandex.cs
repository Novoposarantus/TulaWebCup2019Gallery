using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace API.Yandex
{
	public class Yandex
	{
		private readonly string login;

		private readonly string password;

		private CookieContainer cookies;

		private readonly Uri passportUri = new Uri("https://oauth.yandex.ru/authorize?response_type=code&client_id=62b2b184eeb94321b80a7ba329a99281");

		public Yandex(string login, string password)
		{
			if( login == null )
			{
				throw new ArgumentNullException("login");
			}

			if( login.Length == 0 )
			{
				throw new ArgumentException("login could not be empty", "login");
			}

			if( password == null )
			{
				throw new ArgumentNullException("password");
			}

			if( password.Length == 0 )
			{
				throw new ArgumentException("password could not be empty", "password");
			}

			this.login = login;
			this.password = password;
		}

		public void Authorize()
		{
			HttpWebRequest request = GetRequest(passportUri,
												  new KeyValuePair<string, string>("login_hint", this.login),
												  new KeyValuePair<string, string>("passwd", this.password),
												  new KeyValuePair<string, string>("twoweeks", "yes"),
												  new KeyValuePair<string, string>("retpath", ""));
			try
			{
				using( HttpWebResponse response = (HttpWebResponse)request.GetResponse() )
				{
					if( response.ResponseUri == passportUri )
					{
						throw new YandexException("Неправильная пара логин-пароль! Авторизоваться не удалось.");
					}
				}
			}
			catch( WebException exc )
			{
				throw new YandexException("Чертовщина", exc);
			}
		}

		protected virtual HttpWebRequest GetRequest(Uri url, string method)
		{
			HttpWebRequest request = HttpWebRequest.CreateHttp(url);
			request.Method = method;
			if( cookies == null )
			{
				cookies = new CookieContainer();
			}
			request.CookieContainer = cookies;
			request.KeepAlive = true;
			request.Headers[HttpRequestHeader.AcceptCharset] = Encoding.UTF8.WebName;
			request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
			request.AutomaticDecompression = DecompressionMethods.GZip;
			return request;
		}

		protected virtual HttpWebRequest GetRequest(Uri url, params KeyValuePair<string, string>[] headers)
		{
			HttpWebRequest request = GetRequest(url, WebRequestMethods.Http.Post);
			StringBuilder data = new StringBuilder(1024);
			for( int i = 0; i < headers.Length - 1; i++ )
			{
				data.AppendFormat("{0}={1}&",
								  HttpUtility.HtmlEncode(headers[i].Key),
								  HttpUtility.HtmlEncode(headers[i].Value));
			}
			if( headers.Length > 0 )
			{
				data.AppendFormat("{0}={1}",
								   HttpUtility.HtmlEncode(headers[headers.Length - 1].Key),
								   HttpUtility.HtmlEncode(headers[headers.Length - 1].Value));
			}

			byte[] rawData = Encoding.UTF8.GetBytes(data.ToString());
			request.ContentLength = rawData.Length;
			request.GetRequestStream().Write(rawData, 0, rawData.Length);
			return request;
		}
	}
}
