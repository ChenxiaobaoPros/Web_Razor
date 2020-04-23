﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Razor.Data;
using Web_Razor.Models;

namespace Web_Razor.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Web_Razor.Data.Web_RazorContext _context;

        public IndexModel(Web_Razor.Data.Web_RazorContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        //包含用户在搜索文本框中输入的文本。 SearchString 也有 [BindProperty] 属性。 [BindProperty] 会绑定名称与属性相同的表单值和查询字符串。 在 GET 请求中进行绑定需要 (SupportsGet = true)
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        //包含流派列表。 Genres 使用户能够从列表中选择一种流派。 SelectList 需要 using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Genres { get; set; }

        //包含用户选择的特定流派（例如“西部”）
        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    
    }
}
