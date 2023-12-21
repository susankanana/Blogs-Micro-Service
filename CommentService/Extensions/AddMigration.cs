﻿using CommentService.Data;
using Microsoft.EntityFrameworkCore;
using CommentService.Data;

namespace CommentService.Extensions
{
    public static class AddMigrations
    {

        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate(); //update-database
                }
            }
            return app;
        }
    }
}
