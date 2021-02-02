﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SiteLenroo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 500:
                    ViewData["ErrorMessage"] = "Внутренняя ошибка сервера";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 501:
                    ViewData["ErrorMessage"] = "Не реализовано";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 502:
                    ViewData["ErrorMessage"] = "Сервис временно перегружен";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 503:
                    ViewData["ErrorMessage"] = "Сервис недоступен";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 400:
                    ViewData["ErrorMessage"] = "Некорректный запрос";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 401:
                    ViewData["ErrorMessage"] = "Неавторизованны";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 403:
                    ViewData["ErrorMessage"] = "Запрещено";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 404:
                    ViewData["ErrorMessage"] = "Запрос не найден";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                case 408:
                    ViewData["ErrorMessage"] = "Истекло время запроса";
                    ViewData["ErrorCode"] = statusCode;
                    break;
                default:
                    ViewData["ErrorMessage"] = "Не определенная ошибка";
                    ViewData["ErrorCode"] = statusCode;
                    break;
            }
            return View("ErrorCodePage");
        }
    }
}