﻿using ElevenCategory.Services;
using ElevenNote.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenCategory.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        public IHttpActionResult Get()
        {
            CategoryService categoryService = CreateCategoryService();
            var categorys = categoryService.GetCategorys();
            return Ok(categorys);
        }

        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category))
                return InternalServerError();

            return Ok();
        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(userId);
            return categoryService;
        }

        public IHttpActionResult Get(int id)
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryById(id);
            return Ok(category);
        }

        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(id))
                return InternalServerError();

            return Ok();
        }
    }
}
