using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление книгами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksStorageManager _books;

        public BooksController(IBooksStorageManager books)
        {
            _books = books;
        }


        /// <summary>
        /// Получить список всех книг, и их авторов
        /// </summary>
        /// <returns>
        /// IEnumerable<Book>
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _books.GetAll());
    }
}