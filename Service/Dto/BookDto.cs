using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Dto
{
    public class BookDto
    {
        public int id { get; set; }
        public string bookTitle { get; set; }
        public string genre { get; set; }
        public BookDto()
        {
            
        }
        public BookDto(t_book book)
        {
            this.id = book.id; 
            this.bookTitle = book.bookTitle;
            this.genre = book.genre;
        }
    }
}