﻿using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace STSSimCardProjectReactWithDotNet
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header" +
              "Name:Authorization \r\n" +
              "In:header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"

            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
      {
        new OpenApiSecurityScheme {
          Reference=new OpenApiReference
          {
             Type=ReferenceType.SecurityScheme,
             Id="Bearer"
          },
          Scheme="oauth2",
          Name="Bearer",
          In=ParameterLocation.Header
        },
         new List<string>()
      }
    });
        }

    }
}
