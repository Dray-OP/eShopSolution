﻿using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyWord,int pageIndex = 1,int pageSize =10)
        {
            var request = new GetUsersPagingRequest()
            {  
                Keyword = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var res = await _userApiClient.GetUsersPaging(request);
            return View(res.ResultObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var res = await _userApiClient.RegisterUser(registerRequest);
            if (res.IsSuccessed)
            {
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", res.Message);
            return View(registerRequest);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result= await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id =user.Id,
                    PhoneNumber = user.PhoneNumber
                    
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error","Home");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            var res = await _userApiClient.UpdateUser(request.Id, request);
            if (res.IsSuccessed)
            {
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", res.Message);
            return View(request);
        }
    }

}
