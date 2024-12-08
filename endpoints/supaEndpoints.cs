// using Microsoft.EntityFrameworkCore;
// using Blog;
// using Supabase;

// // https://learn.microsoft.com/en-us/ef/ef6/querying/raw-sql

// namespace supaEndpoints;

// public class SupaEndpoints (Supabase.Client SupabaseInstance)
// {
//     public List<Blog.Blog> GetBlogs()
//     {
//         var blogs = SupabaseInstance.Rpc("SELECT * FROM Blogs");

//         return blogs;
//     }