﻿using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace RMDataManager.App_Start
{
	public class AuthorizationOperationFilter : IOperationFilter
	{
		public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
		{
			if (operation.parameters == null)
			{
				operation.parameters = new List<Parameter>();
			}

			operation.parameters.Add(new Parameter
			{
				name = "Authorization",
				@in = "header",
				description = "access token",
				required = false,
				type = "sstring"
			});
		}
	}
}