using System;
using System.Net;

namespace Clays.App.Domain
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }

		public string ErrorMessage { get; set; }

		public object Result { get; set; }

		public ApiResponse()
		{

		}

		public ApiResponse(HttpStatusCode status,object result = null,string errorMessage = null)
		{
			StatusCode = (int)status;
			Result = result;
			ErrorMessage = errorMessage;
		}
	}

}

