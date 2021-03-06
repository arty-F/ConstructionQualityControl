﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.Extensions.Hosting;
using System.Linq;
using ConstructionQualityControl.Domain.Dtos;

namespace ConstructionQualityControl.Web.Authentication
{
    public static class JWTAuthenticationManager
    {
        private static readonly int keyLength = 32;

        private static string key = "";
        public static string Key
        {
            get
            {
                if (key == null || key == "")
                    throw new NullReferenceException(nameof(Key));
                return key;
            }
            private set { key = value; }
        }

        private static IWebHostEnvironment env;

        public static void Initialize(IWebHostEnvironment env = null)
        {
            JWTAuthenticationManager.env = env;

            if (env == null || env.IsDevelopment())
                Key = string.Join("", Enumerable.Repeat("a", keyLength));
            else if (env.IsProduction())
                Key = GenerateKey(keyLength);
        }

        /// <summary>
        /// Returns generated JSON Web Token for current user.
        /// </summary>
        public static object GetToken(UserReadDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            if (env != null && JWTAuthenticationManager.env.IsProduction())
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(1)).ToUnixTimeSeconds().ToString()));
            }

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)), SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(credentials);
            JwtPayload payload = new JwtPayload(claims);
            var token = new JwtSecurityToken(header, payload);

            return new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                User = user
            };
        }

        private static string GenerateKey(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    sb.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return sb.ToString();
        }
    }
}
