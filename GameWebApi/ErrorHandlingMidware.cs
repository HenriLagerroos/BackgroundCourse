using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ErrorHandlingMidware {
    private readonly RequestDelegate _next;
	public ErrorHandlingMidware(RequestDelegate next) {
		_next = next;
	}
	public async Task Invoke(HttpContext context){
	  try {
		await _next(context);
	  }
	  catch(IdNotFoundException e){
          Console.WriteLine("mois");
		context.Response.StatusCode = (int)HttpStatusCode.NotFound;
	  }
	}
}