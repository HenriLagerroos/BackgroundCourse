using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NotGoodEnoughFilterAttribute : ExceptionFilterAttribute {
	public override void OnException(ExceptionContext context){
		if (context.Exception is NotGoodEnoughException e){
            //Console.WriteLine(e.Message);
            string ex = e.ToString();
			context.Result = new BadRequestObjectResult(ex);
		}
	}
}